using Models;
using DataAccess;

namespace Services;

public static class SystemController
{
    public static void AddUser(User newUser)
    {
        new UserDB(new SqlConnectionFactory()).AddUser(newUser);
    }

    public static bool UserExists(string username)
    {
        return new StaticStorage().UserExists(username);
    }

    public static List<User> GetAllUsers()
    {
        return new UserDB(new SqlConnectionFactory()).GetAllUsers();
    }

    public static void AddTicket(User user, ref Ticket ticket)
    {
        new TicketDB(new SqlConnectionFactory()).AddTicket(user, ref ticket);
    }

    public static bool GetAllTickets(ref List<Ticket> tickets)
    {
        return new TicketDB(new SqlConnectionFactory()).GetAllTickets(ref tickets);
    } 
    public static bool GetUserTickets(User user, ref List<Ticket> userTickets)
    {
        return new TicketDB(new SqlConnectionFactory()).GetUserTickets(user, ref userTickets);
    }

    public static void UpdateTicket(Ticket ticket)
    {
        new TicketDB(new SqlConnectionFactory()).UpdateTicket(ticket);
    }

    public static bool GetTicket(int id, ref Ticket ticket)
    {
        return new TicketDB(new SqlConnectionFactory()).GetTicket(id, ref ticket);
    }

}