using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SQLCallsSample
{
    internal class BulkInsertExecution
    {
        public static async Task ExecuteBulkInsert(string connectionString)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Position", typeof(string));
            dataTable.Rows.Add("John Doe", "Software Engineer");
            dataTable.Rows.Add("Jane Smith", "Project Manager");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "Employees";
                    try
                    {
                        await bulkCopy.WriteToServerAsync(dataTable);
                        Console.WriteLine("Bulk insert completed successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error during bulk insert: {ex.Message}");
                    }
                }
            }
        }
    }
}
