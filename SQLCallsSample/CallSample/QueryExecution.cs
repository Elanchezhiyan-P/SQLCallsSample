using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLCallsSample
{
    internal class QueryExecution
    {
        public static async Task ExecuteQuery(string connectionString)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    await connection.OpenAsync();

                    var sql = "SELECT name, collation_name FROM sys.databases";
                    var command = new SqlCommand(sql, connection);
                    var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine("{0} - {1}", reader.GetString(0), reader.GetString(1));
                    }
                }
            }
            catch (SqlException e) when (e.Number == 1)
            {
                Console.WriteLine($"SQL Error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nDone. Press enter.");
            Console.ReadLine();
        }
    }
}
