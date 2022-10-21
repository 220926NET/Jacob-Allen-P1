using Models;
using Microsoft.Data.SqlClient;

namespace DataAccess;

// ADO.NET : Collection of classes and tools to interact with a variety of data sources in uniform fashion

public class UserDB : IDbAccess<User>
{
    private SqlConnectionFactory _factory;

    public UserDB(SqlConnectionFactory factory)
    {
        _factory = factory;
    }

    public bool Add(User newUser)
    {
        int rows = 0;
        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            //SqlCommand command = new SqlCommand($"INSERT INTO Users (UserName, Password) VALUES ({newUser.Username}, {newUser.Password})", connection);

            using SqlCommand command = new SqlCommand($"INSERT INTO Users (UserName, Password) VALUES (@username, @password)", connection);
            command.Parameters.AddWithValue("@username", newUser.Username);
            command.Parameters.AddWithValue("@password", newUser.Password);

            rows = command.ExecuteNonQuery();
        }
        catch (SqlException)
        {
            Console.WriteLine("Something went wrong connecting to the DB...");
        }

        return rows > 0;
    }

    public List<User> GetAll()
    {

        List<User> users = new List<User>();
        
        try 
        {

            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Users;", connection);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = (int) reader["Id"];
                    string un = (string) reader["UserName"];
                    string pw = (string) reader["Password"];
                    bool manager = (bool) reader ["IsManager"];

                    User currentUser = new User() {
                        Id = id,
                        Username = un,
                        Password = pw,
                        IsManager = manager
                    };

                    users.Add(currentUser);

                }
            }


        }
        catch (SqlException)
        {
            Console.WriteLine("Something went wrong connecting to the DB...");
        }

        return users;

    }

    public User GetById(int id)
    {
        User user = new User();
        try 
        {

            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Id = @id;", connection);
            command.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int userId = (int) reader["Id"];
                    string un = (string) reader["UserName"];
                    string pw = (string) reader["Password"];
                    bool manager = (bool) reader ["IsManager"];

                    
                    user.Id = id;
                    user.Username = un;
                    user.Password = pw;
                    user.IsManager = manager;
                    

                }
            }


        }
        catch (SqlException)
        {
            Console.WriteLine("Something went wrong connecting to the DB...");
        }

        return user;
    }
}