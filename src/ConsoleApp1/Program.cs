using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SenseNet.Search;
using SenseNet.Search.Indexing;

namespace ConsoleApp1
{
    class Program
    {
        private static string ConnectionString =
            "Data Source=.\\SQL2016;Initial Catalog=SN7_Upgrade;Integrated Security=True";

        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            Run().ConfigureAwait(false).GetAwaiter().GetResult();

            if (Debugger.IsAttached)
            {
                Console.Write("Press any key to exit...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        private static async Task Run()
        {
            var scriptRoot = Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                AppDomain.CurrentDomain.BaseDirectory)))), "scripts");

            Console.WriteLine("Reset database...");
            await ResetDatabaseAsync(scriptRoot, ConnectionString);
            Console.WriteLine("Ok.");

            Console.WriteLine("Upgrade Database...");
            await UpgradeDbPart1Async(scriptRoot, ConnectionString);
            Console.WriteLine("Ok.");

            Console.WriteLine("Upgrade Version data");
            await UpgradeVersionDataAsync(ConnectionString);
            Console.WriteLine("Ok.");

            Console.WriteLine("Finalize upgrade...");
            await UpgradeDbPart2Async(scriptRoot, ConnectionString);
            Console.WriteLine("Ok.");
        }

        private static async Task ResetDatabaseAsync(string scriptRoot, string connectionString)
        {
            await ExecuteScriptsAsync(scriptRoot, connectionString, new[]
            {
                "CreateDb\\00-Drop.sql",
                "CreateDb\\01-Install_Security.sql",
                "CreateDb\\02-Install_01_Schema.sql",
                "CreateDb\\03-Install_02_Procs.sql",
                "10-DropConstraintsIndexes.sql",
                "CreateDb\\07-InsertTableData.sql",
            });
        }

        private static async Task UpgradeDbPart1Async(string scriptRoot, string connectionString)
        {
            await ExecuteScriptsAsync(scriptRoot, connectionString, new[]
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
        private static async Task UpgradeDbPart2Async(string scriptRoot, string connectionString)
        {
            await ExecuteScriptsAsync(scriptRoot, connectionString, new[]
            {
                "61-CleanupVersionsTable.sql",
                "90-ReCreateConstraintsIndexes.sql",
                "91-DropOldItems.sql",
            });
        }

        private static async Task UpgradeVersionDataAsync(string connectionString)
        {
            var mappings = await LoadFlatPropertyMappingsAsync(connectionString);
            var versionIds = (await GetVersionIdsFromFlatPropertiesAsync(connectionString)).ToArray();
            var maxCount = versionIds.Length;
            var count = 0;
            foreach (var versionId in versionIds)
            {
                var serializedIndexDoc = await TransformIndexDocumentAsync(versionId, connectionString);

                await UpgradeVersionRecord(versionId, serializedIndexDoc, connectionString, mappings);

                count++;
                if (count % 100 == 0)
                    Console.Write("{0}/{1}        \r", count, maxCount);
            }
        }

        private static async Task UpgradeVersionRecord(int versionId, string serializedIndexDoc, string connectionString,
            Dictionary<int, Dictionary<string, string>> mappings)
        {
            var dynamicPropertyItems = new List<string>();
            var contentListPropertyItems = new List<string>();

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
                                    stringValue = (string)value;
                                else if (column.StartsWith("int_"))
                                    stringValue = value.ToString();
                                else if (column.StartsWith("datetime_"))
                                    stringValue = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                                else if (column.StartsWith("money_"))
                                    stringValue = ((decimal)value).ToString(CultureInfo.InvariantCulture);
                            }

                            if (stringValue != null)
                            {
                                if (propName[0] == '#')
                                    contentListPropertyItems.Add($"{propName}:{stringValue}");
                                else
                                    dynamicPropertyItems.Add($"{propName}:{stringValue}");
                            }
                        }
                    }
                }
            });
            var dynamicProperties = "\r\n" + string.Join("\r\n", dynamicPropertyItems.ToArray()) + "\r\n";
            var contentListProperties = "\r\n" + string.Join("\r\n", contentListPropertyItems.ToArray()) + "\r\n";
            var updateVersionScript =
                "UPDATE tmp_ms_xx_Versions SET " +
                "  DynamicProperties = @DProps, " +
                "  ContentListProperties = @CProps, " +
                "  IndexDocument = @IndxDoc " +
                "WHERE VersionId = @VersionId";
            await SqlScript.ExecuteSqlAsync(updateVersionScript, connectionString, cmd =>
            {
                cmd.Parameters.Add("@VersionId", SqlDbType.Int).Value = versionId;
                cmd.Parameters.Add("@DProps", SqlDbType.NVarChar, -1).Value = dynamicProperties;
                cmd.Parameters.Add("@CProps", SqlDbType.NVarChar, -1).Value = contentListProperties;
                cmd.Parameters.Add("@IndxDoc", SqlDbType.NVarChar, -1).Value = serializedIndexDoc;
            });
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

        private static async Task ExecuteScriptsAsync(string scriptRoot, string connectionString, string[] localPaths)
        {
            foreach (var localPath in localPaths)
                await ExecuteScriptAsync(scriptRoot, localPath, connectionString);
        }

        private static async Task ExecuteScriptAsync(string scriptRoot, string localPath, string connectionString)
        {
            Console.Write("  " + localPath + " ... ");
            await SqlScript.ExecuteFromFileAsync(Path.Combine(scriptRoot, localPath), connectionString);
            Console.WriteLine("Ok");
        }

        private static async Task<string> TransformIndexDocumentAsync(int versionId, string connectionString)
        {
            var indexDocBytes = await LoadIndexDocAsync(versionId, connectionString);
            if (indexDocBytes == null)
                return null;

            var deserialized = Deserialize(indexDocBytes);
            var doc = new IndexDocument2();
            foreach (var item in deserialized)
                doc.Add(item);
            var serialized = doc.Serialize();

            return serialized;
        }
        private static async Task<byte[]> LoadIndexDocAsync(int versionId, string connectionString)
        {
            var sql = "SELECT VersionId, IndexDocument FROM Versions WHERE VersionId = @VersionId";
            byte[] indexDocBytes = null;
            await SqlScript.ExecuteSqlReaderAsync(sql, connectionString, cmd =>
            {
                cmd.Parameters.Add("@VersionId", SqlDbType.Int).Value = versionId;
            }, async reader =>
            {
                if (await reader.ReadAsync())
                    indexDocBytes = reader.IsDBNull(1) ? null : (byte[])reader.GetValue(1);
            });
            return indexDocBytes;
        }
        private static IndexDocument Deserialize(byte[] serializedIndexDocument)
        {
            var docStream = new MemoryStream(serializedIndexDocument);

            var formatter = new BinaryFormatter();
            var indxDoc = (IndexDocument)formatter.Deserialize(docStream);
            return indxDoc;
        }

        /* =============================================================================== INDEX DOCUMENT SERIALIZATION */

        private class IndexFieldJsonConverter : JsonConverter<IndexField>
        {
            public override void WriteJson(JsonWriter writer, IndexField value, JsonSerializer serializer)
            {
                writer.WriteStartObject();

                writer.WritePropertyName("Name");
                writer.WriteValue(value.Name);
                writer.WritePropertyName("Type");
                writer.WriteValue(value.Type.ToString());
                if (value.Mode != IndexingMode.Default)
                {
                    writer.WritePropertyName("Mode");
                    writer.WriteValue(value.Mode.ToString());
                }
                if (value.Store != IndexStoringMode.Default)
                {
                    writer.WritePropertyName("Store");
                    writer.WriteValue(value.Store.ToString());
                }
                if (value.TermVector != IndexTermVector.Default)
                {
                    writer.WritePropertyName("TermVector");
                    writer.WriteValue(value.TermVector.ToString());
                }
                writer.WritePropertyName("Value");
                switch (value.Type)
                {
                    case IndexValueType.String:
                        writer.WriteValue(value.StringValue);
                        break;
                    case IndexValueType.Bool:
                        writer.WriteValue(value.BooleanValue);
                        break;
                    case IndexValueType.Int:
                        writer.WriteValue(value.IntegerValue);
                        break;
                    case IndexValueType.Long:
                        writer.WriteValue(value.LongValue);
                        break;
                    case IndexValueType.Float:
                        writer.WriteValue(value.SingleValue);
                        break;
                    case IndexValueType.Double:
                        writer.WriteValue(value.DoubleValue);
                        break;
                    case IndexValueType.DateTime:
                        writer.WriteValue(value.DateTimeValue);
                        break;
                    case IndexValueType.StringArray:
                        writer.WriteStartArray();
                        writer.WriteRaw("\"" + string.Join("\",\"", value.StringArrayValue) + "\"");
                        writer.WriteEndArray();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                writer.WriteEndObject();
            }

            public override IndexField ReadJson(JsonReader reader, Type objectType, IndexField existingValue,
                bool hasExistingValue,
                JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

        [Serializable]
        public class IndexDocument2 : IEnumerable<IndexField>
        {
            private readonly Dictionary<string, IndexField> _fields = new Dictionary<string, IndexField>();

            public int VersionId => GetIntegerValue(IndexFieldName.VersionId);
            public string Version => GetStringValue(IndexFieldName.Version);

            public string GetStringValue(string fieldName)
            {
                if (!_fields.TryGetValue(fieldName, out var field))
                    return default(string);
                return field.StringValue;
            }
            public int GetIntegerValue(string fieldName)
            {
                if (!_fields.TryGetValue(fieldName, out var field))
                    return default(int);
                return field.IntegerValue;
            }

            public void Add(IndexField field)
            {
                _fields[field.Name] = field;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            /// <inheritdoc />
            public IEnumerator<IndexField> GetEnumerator()
            {
                return _fields.Values.GetEnumerator();
            }

            /* =========================================================================================== */

            private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new IndexFieldJsonConverter() },
                NullValueHandling = NullValueHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                Formatting = Formatting.Indented
            };

            public string Serialize()
            {
                using (var writer = new StringWriter())
                {
                    JsonSerializer.Create(SerializerSettings).Serialize(writer, this);
                    var serializedDoc = writer.GetStringBuilder().ToString();
                    return serializedDoc;
                }
            }
        }

    }
}