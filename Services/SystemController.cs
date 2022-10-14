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

    public static bool GetUserTickets(User user, ref List<Ticket> userTickets)
    {
        return new TicketDB(new SqlConnectionFactory()).GetUserTickets(user, ref userTickets);
    }
}