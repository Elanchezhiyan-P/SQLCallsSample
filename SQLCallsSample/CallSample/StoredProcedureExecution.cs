using System.Data.SqlClient;
using System;
using System.Threading.Tasks;
using System.Data;

namespace SQLCallsSample
{
    internal class StoredProcedureExecution
    {
        public static async Task ExecuteStoredProcedure(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    //define the query text
                    string query = @"EXEC [dbo].[fnGetTotalEmployees] @empID = @empID";

                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);

                    //parameter value will be set from command line
                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@empID";
                    param1.SqlDbType = SqlDbType.Int;
                    param1.Value = int.Parse("01");

                    //pass parameter to the SQL Command
                    cmd.Parameters.Add(param1);

                    //open connection
                    await conn.OpenAsync();

                    //execute the SQLCommand
                    var procedureResult = await cmd.ExecuteScalarAsync();

                    Console.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);
                    Console.WriteLine("Retrieved result:");


                    //display retrieved result
                    Console.WriteLine("Total employees for location id={0}: {1}", "01", procedureResult.ToString());
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
