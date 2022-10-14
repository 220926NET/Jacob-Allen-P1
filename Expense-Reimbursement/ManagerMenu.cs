using Models;
using Services;

public class ManagerMenu
{
    User manager;

    public ManagerMenu(User user)
    {
        manager = user;
        StartMenu();
    }

    void StartMenu()
    {
        Console.WriteLine($"Welcome Manager {manager.Username}");
        Console.WriteLine("Please choose an option below:");
        Console.WriteLine("[1] Update expense reports");
        Console.WriteLine("[2] View previous expense reports");
        Console.WriteLine("[q] Logout");

        
        bool isValid = false;

        while(!isValid)
        {
            string? input = Console.ReadLine();
            switch (input)
            {
                case null:
                    Console.WriteLine("Please enter a valid input.");
                    isValid = false;
                    break;
                case "1":
                    Console.WriteLine("Updating expense report");
                    Console.WriteLine("Enter Ticket Id:");
                    int id;
                    bool validId = int.TryParse(Console.ReadLine(), out id);
                    if (validId)
                    {
                        Ticket ticket = new Ticket();
                        bool foundTicket = GetTicket(id, ref ticket);

                        if (foundTicket)
                        {
                            Console.WriteLine("Enter new Status for the Ticket");
                            string? status = Console.ReadLine();
                            ticket.CurrentStatus = status ?? "Pending";
                            UpdateTicket(ticket);
                        }
                    }
                    isValid = true;
                    break;
                case "2":
                    Console.WriteLine("View previous reports");
                    List<Ticket> tickets = new List<Ticket>();
                    bool success = GetAllTickets(ref tickets);

                    if (success)
                    {
                        foreach (Ticket ticket in tickets)
                        {
                            Console.WriteLine(ticket.ToString());
                        }
                    }
                    else Console.WriteLine("There are no tickets.");
                    Console.ReadLine();
                    isValid = true;
                    break;
                case "q":
                    Console.WriteLine("Logging Out");
                    Console.ReadLine();
                    isValid = true;
                    break;
                default:
                    Console.WriteLine("Please enter a valid input.");
                    isValid = false;
                    break;
            }
        }
    }

    bool GetAllTickets(ref List<Ticket> tickets)
    {
        return SystemController.GetAllTickets(ref tickets);
    }

    void UpdateTicket(Ticket ticket)
    {
        SystemController.UpdateTicket(ticket);
    }

    bool GetTicket(int id, ref Ticket ticket)
    {
        return SystemController.GetTicket(id, ref ticket);
    }
}