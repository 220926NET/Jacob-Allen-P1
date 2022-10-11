using Models;
using Microsoft.Data.SqlClient;

namespace DataAccess;

// ADO.NET : Collection of classes and tools to interact with a variety of data sources in uniform fashion

public class DBRepo
{
    public void AddUser(User newUser)
    {

        SqlConnection connection = new SqlConnection("Server=tcp:ja-revature.database.windows.net,1433;Initial Catalog=Project1DB;Persist Security Info=False;User ID=jallen;Password=Openme123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand($"INSERT INTO Users (UserName, Password) VALUES ({newUser.Username}, {newUser.Password})", connection);

            command = new SqlCommand($"INSERT INTO Users (UserName, Password) VALUES (@username, @password)", connection);
            command.Parameters.AddWithValue("@username", newUser.Username);
            command.Parameters.AddWithValue("@password", newUser.Password);

            command.ExecuteNonQuery();
        }
        catch (SqlException)
        {
            Console.WriteLine("Something went wrong connecting to the DB...");
        }
        finally
        {
            connection.Close();
        }
    }

    public List<User> GetAllUsers()
    {

        List<User> users = new List<User>();


        /* 
        Connecting and interacting with DB...
        1. Connect to the db
            - Tell the program where to go find the dbServer, and the credentials (provided via connectionstring)
            - Open the connection
        2. Assemble the query you are interested in running
        3. Execute those statements
        4. Get the result
        5. Process the result, so rest of the application can understand it
        6. Close the connection
        7. Return the result to the caller
        */

        SqlConnection connection = new SqlConnection("Server=tcp:ja-revature.database.windows.net,1433;Initial Catalog=Project1DB;Persist Security Info=False;User ID=jallen;Password=Openme123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        
        try 
        {

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
                        Username = un,
                        Password = pw,
                        isManager = manager
                    };

                    users.Add(currentUser);

                }
            }


        }
        catch (SqlException)
        {
            Console.WriteLine("Something went wrong connecting to the DB...");
        }
        finally
        {
            connection.Close();
        }


        return users;

    }
}