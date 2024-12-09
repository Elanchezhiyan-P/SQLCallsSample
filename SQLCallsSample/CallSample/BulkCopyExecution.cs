using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SQLCallsSample
{
    internal class BulkCopyExecution
    {
        public static async Task ExecuteBulkCopy(string connectionString)
        {
            // Create a new DataTable to hold the data to be copied
            DataTable dataTable = new DataTable();

            // Define the columns of the DataTable (this should match the destination table structure)
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
                        Console.WriteLine("Bulk copy completed successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error during bulk copy: {ex.Message}");
                    }
                }
            }
        }
    }
}
