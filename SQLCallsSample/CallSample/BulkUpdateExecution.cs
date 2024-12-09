using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SQLCallsSample
{
    internal class BulkUpdateExecution
    {
        public static async Task ExecuteBulkUpdate(string connectionString)
        {
            // Sample data for bulk update (this should match the target table structure)
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("EmployeeId", typeof(int));
            dataTable.Columns.Add("Position", typeof(string));
            dataTable.Rows.Add(1, "Senior Software Engineer");
            dataTable.Rows.Add(2, "Senior Project Manager");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                foreach (DataRow row in dataTable.Rows)
                {
                    string updateCommand = "UPDATE Employees SET Position = @Position WHERE EmployeeId = @EmployeeId";
                    using (SqlCommand command = new SqlCommand(updateCommand, connection))
                    {
                        command.Parameters.AddWithValue("@Position", row["Position"]);
                        command.Parameters.AddWithValue("@EmployeeId", row["EmployeeId"]);

                        try
                        {
                            await command.ExecuteNonQueryAsync();
                            Console.WriteLine("Bulk update completed successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error during bulk update: {ex.Message}");
                        }
                    }
                }
            }
        }
    }
}
