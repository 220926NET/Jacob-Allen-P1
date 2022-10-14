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

            using SqlCommand insertCommand = new SqlCommand($"INSERT INTO Tickets ([UserId], [Description], [Amount], [Status]) OUTPUT INSERTED.Id VALUES (@userid, @description, @amount, @status);", connection);
            insertCommand.Parameters.AddWithValue("@userid", ticket.UserId);
            insertCommand.Parameters.AddWithValue("@description", ticket.Description);
            insertCommand.Parameters.AddWithValue("@amount", ticket.Amount);
            insertCommand.Parameters.AddWithValue("@status", ticket.CurrentStatus.ToString());

            int id = (int) insertCommand.ExecuteScalar();

            using SqlCommand selectCommand = new SqlCommand($"SELECT * FROM Tickets WHERE Id = @id", connection);
            selectCommand.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = selectCommand.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();

                ticket.Id = (int) reader["Id"];
                // Console.WriteLine(reader["DateSubmitted"].GetType());
                ticket.DateSubmitted = DateOnly.FromDateTime((DateTime) reader["DateSubmitted"]);
            }
            
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

    public bool GetUserTickets(User user, ref List<Ticket> userTickets)
    {
        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Tickets WHERE UserId = @userid;", connection);
            command.Parameters.AddWithValue("@userid", user.Id);

            SqlDataReader reader = command.ExecuteReader();

            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    Ticket ticket = new Ticket() {
                        Id = (int) reader["Id"],
                        UserId = (int) reader["UserId"],
                        Description = (string) reader["Description"],
                        Amount = (decimal) reader["Amount"],
                        DateSubmitted = DateOnly.FromDateTime((DateTime) reader["DateSubmitted"]),
                        CurrentStatus = (string) reader["Status"]
                    };

                    userTickets.Add(ticket);
                }
            }
        }
        catch(SqlException)
        {
            Console.WriteLine("Something went wrong connecting to the DB...");
        }

        if (userTickets.Count > 0)
        {
            return true;
        }

        return false;
    }
}