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

    public static bool LoginCheck(string? username, string? password, ref User loginUser)
    {
        List<User> users = SystemController.GetAllUsers();
        bool loginSuccessful = false;

        foreach (User user in users)
        {
            if (user.Username == username && user.Password == password)
            {
                loginSuccessful = true;
                loginUser = user;
            }
        }

        return loginSuccessful;
    }

    public static bool CheckUserExists(string username)
    {
        bool validInput = false;
        List<User> users = SystemController.GetAllUsers();

        foreach (User user in users)
        {
            if (username == user.Username)
            {
                Console.WriteLine("Username already exists, please enter a new one.");
                validInput = false;
                break;
            }

            validInput = true;
        }
        return validInput;
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