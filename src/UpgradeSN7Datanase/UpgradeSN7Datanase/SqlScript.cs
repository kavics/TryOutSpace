﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class SqlScript
    {
        public static async Task ExecuteFromFileAsync(string path, string connectionString)
        {
            using (var reader = new StreamReader(path))
            using (var sqlReader = new SqlScriptReader(reader))
                await ExecuteSqlAsync(sqlReader, connectionString);
        }

        public static async Task ExecuteFromTextAsync(string text, string connectionString)
        {
            using (var reader = new StringReader(text))
            using (var sqlReader = new SqlScriptReader(reader))
                await ExecuteSqlAsync(sqlReader, connectionString);
        }

        private static async Task ExecuteSqlAsync(SqlScriptReader sqlReader, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                while (sqlReader.ReadScript())
                {
                    var script = sqlReader.Script;
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = script;
                        command.CommandType = CommandType.Text;
                        await command.ExecuteNonQueryAsync(CancellationToken.None);
                    }
                }
            }
        }

        public static async Task ExecuteSqlAsync(string script, string connectionString,
            Action<SqlCommand> setParams)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = script;
                    command.CommandType = CommandType.Text;
                    setParams(command);
                    await command.ExecuteNonQueryAsync(CancellationToken.None);
                }
            }
        }


        public static async Task ExecuteSqlReaderAsync(string script, string connectionString,
            Func<SqlDataReader, Task> callback)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = script;
                    command.CommandType = CommandType.Text;
                    using (var reader = await command.ExecuteReaderAsync(CancellationToken.None))
                        await callback(reader);
                }
            }
        }
        public static async Task ExecuteSqlReaderAsync(string script, string connectionString,
            Action<SqlCommand> setParams,
            Func<SqlDataReader, Task> callback)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = script;
                    command.CommandType = CommandType.Text;
                    setParams(command);
                    using (var reader = await command.ExecuteReaderAsync(CancellationToken.None))
                        await callback(reader);
                }
            }
        }

    }
}