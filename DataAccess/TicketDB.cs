using Models;
using Microsoft.Data.SqlClient;

namespace DataAccess;

// ADO.NET : Collection of classes and tools to interact with a variety of data sources in uniform fashion

public class TicketDB : ITicketDB
{
    private SqlConnectionFactory _factory;

    public TicketDB(SqlConnectionFactory factory)
    {
        _factory = factory;
    }

    public List<Ticket> GetAll()
    {
        List<Ticket> tickets = new List<Ticket>();

        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Tickets", connection);

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
                        DateSubmitted = (DateTime) reader["DateSubmitted"],
                        CurrentStatus = (string) reader["Status"]
                    };

                    tickets.Add(ticket);
                }
            }
        }
        catch(SqlException)
        {
            Console.WriteLine("Something went wrong connecting to the DB...");
        }

        return tickets;
    }
    public Ticket GetById(int id)
    {
        Ticket ticket = new Ticket();

        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Tickets WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = command.ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                ticket.Id = (int)reader["Id"];
                ticket.UserId = (int)reader["UserId"];
                ticket.Description = (string)reader["Description"];
                ticket.Amount = (decimal)reader["Amount"];
                ticket.DateSubmitted = (DateTime)reader["DateSubmitted"];
                ticket.CurrentStatus = (string)reader["Status"];

            }
        }
        catch(SqlException)
        {
            Console.WriteLine("Something went wrong connecting to the DB...");
        }

        return ticket;
    }
    public bool Add(ref Ticket ticket)
    {
        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

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
                ticket.DateSubmitted = (DateTime) reader["DateSubmitted"];

                return true;
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

        return false;
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
                        DateSubmitted = (DateTime) reader["DateSubmitted"],
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

    public bool GetPendingTickets(ref List<Ticket> pendingTickets)
    {
        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Tickets WHERE Status = 'Pending'", connection);

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
                        DateSubmitted = (DateTime) reader["DateSubmitted"],
                        CurrentStatus = (string) reader["Status"]
                    };

                    pendingTickets.Add(ticket);
                }
            }
        }
        catch(SqlException)
        {
            Console.WriteLine("Something went wrong connecting to the DB...");
        }

        if (pendingTickets.Count > 0)
        {
            return true;
        }

        return false;
    }

    public void UpdateTicket(Ticket ticket)
    {
        try
        {
            using SqlConnection connection = _factory.GetConnection();
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE Tickets SET Status = @status WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@status", ticket.CurrentStatus.ToString());
            command.Parameters.AddWithValue("@id", ticket.Id);

            command.ExecuteNonQuery();
        }
        catch(SqlException)
        {
            Console.WriteLine("Something went wrong connecting to the DB...");
        }
    }
}