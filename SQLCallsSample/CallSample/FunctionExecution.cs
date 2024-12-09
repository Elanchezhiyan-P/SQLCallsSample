using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SQLCallsSample
{
    internal class FunctionExecution
    {
        public static async Task ExecuteFunction(string connectionString)
        {
            int empID, locationID;
            string empCode, empFirstName, empLastName, locationCode, locationDescr;
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT * FROM [dbo].[fnGetEmployeeInfo](@empID);";

                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);

                    //parameter value will be set from command line
                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@empID";
                    param1.SqlDbType = SqlDbType.Int;
                    param1.Value = int.Parse("1");

                    //pass parameter to the SQL Command
                    cmd.Parameters.Add(param1);

                    //open connection
                    await conn.OpenAsync();

                    //execute the SQLCommand
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    Console.WriteLine(Environment.NewLine + "Retrieving data from database..." + Environment.NewLine);
                    Console.WriteLine("Retrieved records:");

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (await dr.ReadAsync())
                        {
                            empID = dr.GetInt32(0);
                            empCode = dr.GetString(1);
                            empFirstName = dr.GetString(2);
                            empLastName = dr.GetString(3);
                            locationID = dr.GetInt32(4);
                            locationCode = dr.GetString(5);
                            locationDescr = dr.GetString(6);

                            //display retrieved record
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6}", empID.ToString(), empCode, empFirstName, empLastName, locationID, locationCode, locationDescr);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }

                    //close data reader
                    dr.Close();
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
