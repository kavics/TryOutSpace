using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        private static string ConnectionString = "Data Source=.\\SQL2016;Initial Catalog=SN7_Upgrade;Integrated Security=True";

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

            Console.WriteLine("Upgrade SN Schema...");
            UpgradeSnSchema(scriptRoot, ConnectionString);
            Console.WriteLine("Ok.");

            if (Debugger.IsAttached)
            {
                Console.Write("Press any key to exit...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        private static void UpgradeSnSchema(string scriptRoot, string connectionString)
        {
            ExecuteScripts(scriptRoot, connectionString, new[]
            {
                "100-Create-new-SnSchema.sql",
                "101-UpgradeSchema.sql",
            });
        }

        private static void ResetDatabase(string scriptRoot, string connectionString)
        {
            ExecuteScripts(scriptRoot, connectionString, new[]
            {
                "CreateDb\\01-Install_Security.sql",
                "CreateDb\\02-Install_01_Schema.sql",
                "CreateDb\\03-Install_02_Procs.sql",
                "CreateDb\\04-CustomItems.sql",
                "CreateDb\\05-InsertOldSchemaData.sql",
            });
        }

        private static void ExecuteScripts(string scriptRoot, string connectionString, string[] localPaths)
        {
            foreach(var localPath in localPaths)
                ExecuteScript(scriptRoot, localPath, connectionString);
        }
        private static void ExecuteScript(string scriptRoot, string localPath, string connectionString)
        {
            Console.Write("  " + localPath + " ... ");
            SqlScript.ExecuteFromFile(Path.Combine(scriptRoot, localPath), connectionString)
                .GetAwaiter().GetResult();
            Console.WriteLine("Ok");
        }
    }
}