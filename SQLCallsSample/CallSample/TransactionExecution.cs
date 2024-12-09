using System;
using System.Data.SqlClient;

namespace SQLCallsSample
{
    internal class TransactionExecution
    {
        public static void ExecuteTransaction(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Begin a transaction
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;

                            command.CommandText = "INSERT INTO Employees (Name, Position) VALUES (@Name, @Position)";
                            command.Parameters.AddWithValue("@Name", "John Doe");
                            command.Parameters.AddWithValue("@Position", "Software Engineer");

                            // Execute the command
                            command.ExecuteNonQuery();

                            transaction.Commit();
                            Console.WriteLine("Transaction committed successfully.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // If an error occurs, roll back the transaction
                        Console.WriteLine($"Error: {ex.Message}");
                        try
                        {
                            transaction.Rollback();
                            Console.WriteLine("Transaction rolled back.");
                        }
                        catch (Exception rollbackEx)
                        {
                            Console.WriteLine($"Rollback failed: {rollbackEx.Message}");
                        }
                    }
                }
            }
        }
    }
}
