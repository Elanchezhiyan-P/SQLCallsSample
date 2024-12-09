using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SQLCallsSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "DESKTOP-1680TAC",
                UserID = "sa",
                Password = "AitAdmin",
                InitialCatalog = "Ged-Prod"
            };
            var connectionString = builder.ConnectionString;

            await QueryExecution.ExecuteQuery(connectionString);

            await FunctionExecution.ExecuteFunction(connectionString);

            await StoredProcedureExecution.ExecuteStoredProcedure(connectionString);

            TransactionExecution.ExecuteTransaction(connectionString);

            await BulkCopyExecution.ExecuteBulkCopy(connectionString);

            await BulkInsertExecution.ExecuteBulkInsert(connectionString);

            await BulkUpdateExecution.ExecuteBulkUpdate(connectionString);

            await BulkDeleteExecution.ExecuteBulkDelete(connectionString);
        }
    }
}
