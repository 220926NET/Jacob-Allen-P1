using Microsoft.Data.SqlClient;

namespace DataAccess;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private const string _connectionString = Secrets.connectionString;

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}