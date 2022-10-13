using Microsoft.Data.SqlClient;

namespace DataAccess;

public class SqlConnectionFactory
{
    private const string _connectionString = "Server=tcp:ja-revature.database.windows.net,1433;Initial Catalog=Project1DB;Persist Security Info=False;User ID=jallen;Password=Openme123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}