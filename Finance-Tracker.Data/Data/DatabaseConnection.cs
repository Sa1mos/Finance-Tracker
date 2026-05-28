using Npgsql;

namespace FinanceTracker.Data
{
    public class DatabaseConnection
    {
        private readonly string ConnectionString = null!;

        public DatabaseConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public NpgsqlDataSource CreateDataSource()
        {
            return NpgsqlDataSource.Create(ConnectionString);
        }
    }
}