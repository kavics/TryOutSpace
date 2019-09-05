using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        private static string ConnectionString =
            "Data Source=.\\SQL2016;Initial Catalog=SN7_Upgrade;Integrated Security=True";

        static void Main(string[] args)
        {
            var scriptRoot = Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                AppDomain.CurrentDomain.BaseDirectory)))), "scripts");

            Console.WriteLine("Reset database...");
            ResetDatabase(scriptRoot, ConnectionString);
            Console.WriteLine("Ok.");

            Console.WriteLine("Upgrade DB Schema...");
            UpgradeDbPart1(scriptRoot, ConnectionString);
            Console.WriteLine("Ok.");

            //Console.WriteLine("Upgrade FlatProperties");
            //UpgradeFlatProperties(ConnectionString).ConfigureAwait(false).GetAwaiter().GetResult();
            //Console.WriteLine("Ok.");

            if (Debugger.IsAttached)
            {
                Console.Write("Press any key to exit...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        private static void ResetDatabase(string scriptRoot, string connectionString)
        {
            ExecuteScripts(scriptRoot, connectionString, new[]
            {
                "CreateDb\\01-Install_Security.sql",
                "CreateDb\\02-Install_01_Schema.sql",
                "CreateDb\\03-Install_02_Procs.sql",
                "CreateDb\\05-InsertOldSchemaData.sql",
                "CreateDb\\06-InsertFlatProperties.sql",
                "CreateDb\\07-InsertTableData.sql",
            });
        }

        private static void UpgradeDbPart1(string scriptRoot, string connectionString)
        {
            ExecuteScripts(scriptRoot, connectionString, new[]
            {
                "10-DropConstraintsIndexes.sql",
                "20-CreateNewSnSchema.sql",
                "21-UpgradeSchema.sql",
                "30-CreateNewLongTextProperties.sql",
                "31-UpgradeLongTextProperties.sql",
                "40-DateTime2.sql",
                "50-AlterViews.sql",
                "60-RebuildVersionsTable.sql",
            });
        }
        private static void UpgradeDbPart2(string scriptRoot, string connectionString)
        {
            ExecuteScripts(scriptRoot, connectionString, new[]
            {
                "61-CleanupVersionsTable.sql",
                "90-ReCreateConstraintsIndexes.sql",
                "91-DropOldItems.sql",
            });
        }

        private static async Task UpgradeFlatProperties(string connectionString)
        {
            var mappings = await LoadFlatPropertyMappingsAsync(connectionString);
            var versionIds = await GetVersionIdsFromFlatPropertiesAsync(connectionString);
            foreach (var versionId in versionIds)
            {
                var dynamicPropertyItems = new List<string>();

                var script = "SELECT * FROM FlatProperties WHERE VersionId = " + versionId;
                await SqlScript.ExecuteSqlReaderAsync(script, connectionString, async reader =>
                {
                    while (await reader.ReadAsync(CancellationToken.None))
                    {
                        var page = reader.GetInt32(2);
                        
                        for (var i = 3; i < reader.FieldCount; i++)
                        {
                            var value = reader.GetValue(i);
                            if (value != DBNull.Value)
                            {
                                var column = reader.GetName(i);

                                string stringValue = null;
                                if (mappings[page].TryGetValue(column, out var propName))
                                {
                                    if (column.StartsWith("nvarchar_"))
                                        stringValue = (string) value;
                                    else if (column.StartsWith("int_"))
                                        stringValue = value.ToString();
                                    else if (column.StartsWith("datetime_"))
                                        stringValue = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                                    else if (column.StartsWith("money_"))
                                        stringValue = ((decimal) value).ToString(CultureInfo.InvariantCulture);
                                }

                                if(stringValue!=null)
                                    dynamicPropertyItems.Add($"{propName}:{stringValue}");
                            }
                        }
                    }
                });

                var dynamicProperties = "\r\n" + string.Join("\r\n", dynamicPropertyItems.ToArray()) + "\r\n";
                Console.WriteLine("VersionId: " + versionId);
                Console.WriteLine(dynamicProperties);
                Console.WriteLine();
            }
        }

        private static async Task<Dictionary<int, Dictionary<string, string>>> LoadFlatPropertyMappingsAsync(
            string connectionString)
        {
            var mappings = new Dictionary<int, Dictionary<string, string>>();

            var sql = "SELECT [Name], [Page], [Column] FROM PropertyInfoView WHERE [Table] = 'Flat'";
            await SqlScript.ExecuteSqlReaderAsync(sql, connectionString, async reader =>
            {
                while (await reader.ReadAsync(CancellationToken.None))
                {
                    var name = reader.GetString(0);
                    var page = reader.GetInt32(1);
                    var col = reader.GetString(2);
                    if (!mappings.TryGetValue(page, out var mappingPage))
                    {
                        mappingPage = new Dictionary<string, string>();
                        mappings.Add(page, mappingPage);
                    }
                    mappingPage.Add(col, name);
                }
            });

            return mappings;
        }
        private static async Task<IEnumerable<int>> GetVersionIdsFromFlatPropertiesAsync(string connectionString)
        {
            var result = new List<int>();

            var sql = "SELECT DISTINCT VersionId FROM FlatProperties";
            await SqlScript.ExecuteSqlReaderAsync(sql, connectionString, async reader =>
            {
                while (await reader.ReadAsync(CancellationToken.None))
                    result.Add(reader.GetInt32(0));
            });

            return result;
        }

        private static void ExecuteScripts(string scriptRoot, string connectionString, string[] localPaths)
        {
            foreach (var localPath in localPaths)
                ExecuteScript(scriptRoot, localPath, connectionString);
        }

        private static void ExecuteScript(string scriptRoot, string localPath, string connectionString)
        {
            Console.Write("  " + localPath + " ... ");
            SqlScript.ExecuteFromFile(Path.Combine(scriptRoot, localPath), connectionString)
                .ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine("Ok");
        }
    }
}