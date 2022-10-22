using Models;
using DataAccess;

namespace Services;

public class SystemController : IService
{
    private readonly IDbAccess<User> _repo;

    public SystemController(IDataAccessFactory dataAccessFactory)
    {
        _repo = dataAccessFactory.GetUserDB();
    }
    public static void PromptContinue()
    {
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }

    public bool AddUser(User newUser)
    {
        bool valid = CheckUserExists(newUser.Username);
        if (valid)
        {
            _repo.Add(newUser);
        }

        return valid;
    }

    public static List<User> GetAllUsers()
    {
        return new UserDB(new SqlConnectionFactory()).GetAll();
    }

    public static User GetUser(int id)
    {
        return new UserDB(new SqlConnectionFactory()).GetById(id);
    }

    public static bool LoginCheck(ref User loginUser)
    {
        List<User> users = SystemController.GetAllUsers();
        bool loginSuccessful = false;

        foreach (User user in users)
        {
            if (user.Username == loginUser.Username && user.Password == loginUser.Password)
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

    public static bool GetPendingTickets(ref List<Ticket> tickets)
    {
        return new TicketDB(new SqlConnectionFactory()).GetPendingTickets(ref tickets);
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