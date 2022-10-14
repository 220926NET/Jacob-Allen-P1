using Models;
using Microsoft.Data.SqlClient;

namespace DataAccess;

// ADO.NET : Collection of classes and tools to interact with a variety of data sources in uniform fashion

public class TicketDB
{
    private SqlConnectionFactory _factory;

    public TicketDB(SqlConnectionFactory factory)
    {
        _factory = factory;
    }

    public void AddTicket(User user, ref Ticket ticket)
    {
        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            //SqlCommand command = new SqlCommand($"INSERT INTO Tickets (UserId, Description, Amount) VALUES ({newUser.Username}, {newUser.Password})", connection);

            using SqlCommand command = new SqlCommand($"INSERT INTO Tickets ([UserId], [Description], [Amount], [Status]) OUTPUT INSERTED.Id VALUES (@userid, @description, @amount, @status);", connection);
            command.Parameters.AddWithValue("@userid", user.UserId);
            command.Parameters.AddWithValue("@description", ticket.Description);
            command.Parameters.AddWithValue("@amount", ticket.Amount);
            command.Parameters.AddWithValue("@status", ticket.CurrentStatus.ToString());

            int ticketId = (int) command.ExecuteScalar();

            ticket.Id = ticketId;
        }
        catch (InvalidCastException)
        {
            Console.WriteLine("Invalid cast");
        }
        catch (SqlException)
        {
            Console.WriteLine("Something went wrong connecting to the DB...");
        }
    }

    public List<User> GetAllUsers()
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
}