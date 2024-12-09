using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SQLCallsSample
{
    internal class BulkDeleteExecution
    {
        public static async Task ExecuteBulkDelete(string connectionString)
        {
            // Sample data for bulk delete (this should match the target table structure)
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("EmployeeId", typeof(int));
            dataTable.Rows.Add(1);
            dataTable.Rows.Add(2);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                foreach (DataRow row in dataTable.Rows)
                {
                    string deleteCommand = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";
                    using (SqlCommand command = new SqlCommand(deleteCommand, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", row["EmployeeId"]);

                        try
                        {
                            await command.ExecuteNonQueryAsync();
                            Console.WriteLine("Bulk delete completed successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error during bulk delete: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}
