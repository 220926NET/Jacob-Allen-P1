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
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"              Welcome Manager {manager.Username}");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Please choose an option below:");
            Console.WriteLine("[1] Update expense reports");
            Console.WriteLine("[2] View previous expense reports");
            Console.WriteLine("[3] Print all users");
            Console.WriteLine("[q] Logout");
            Console.WriteLine("");


            bool isValid = false;

            while (!isValid)
            {
                string? input = Console.ReadLine();
                switch (input)
                {
                    case null:
                        Console.WriteLine("Please enter a valid input.");
                        isValid = false;
                        break;
                    case "1":
                        Console.Clear();
                        UpdateTickets();
                        isValid = true;
                        break;
                    case "2":
                        Console.Clear();
                        List<Ticket> tickets = new List<Ticket>();
                        bool success = GetAllTickets(ref tickets);

                        if (success)
                        {
                            tickets[0].PrintHeader();
                            foreach (Ticket ticket in tickets)
                            {
                                Console.WriteLine(ticket.ToString());
                            }
                        }
                        else Console.WriteLine("There are no tickets.");
                        SystemController.PromptContinue();
                        isValid = true;
                        break;
                    case "3":
                        Console.Clear();
                        PrintUsersInfo();
                        SystemController.PromptContinue();
                        isValid = true;
                        break;
                    case "q":
                        isValid = true;
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid input.");
                        isValid = false;
                        break;
                }
            }
        }
    }

    void PrintUsersInfo()
    {
        List<User> users = SystemController.GetAllUsers();
        users[0].PrintHeader();

        foreach (User user in users)
        {
            Console.WriteLine(user.ToString());
        }
    }

    bool GetAllTickets(ref List<Ticket> tickets)
    {
        return SystemController.GetAllTickets(ref tickets);
    }

    bool GetPendingTickets(ref List<Ticket> tickets)
    {
        return SystemController.GetPendingTickets(ref tickets);
    }

    void UpdateTickets()
    {

        List<Ticket> pendingTickets = new List<Ticket>();
        bool success = GetPendingTickets(ref pendingTickets);

        if (success)
        {
            pendingTickets[0].PrintHeader();

            foreach(Ticket ticket in pendingTickets)
            {
                Console.WriteLine(ticket.ToString());
            }

            Console.WriteLine("\nEnter Ticket Id:");
            int id;
            bool validId = int.TryParse(Console.ReadLine(), out id);
            if (validId)
            {
                Ticket foundTicket = new Ticket();
                bool found = false;

                foreach(Ticket ticket in pendingTickets)
                {
                    if(ticket.Id == id) 
                    {
                        foundTicket = ticket;
                        found = true;
                    }
                }

                if (found)
                {
                    Console.WriteLine("Enter new status for the ticket: 'Approved' or 'Denied'");
                    string? status = Console.ReadLine();
                    if (status == "Approved" || status == "Denied")
                    {
                        foundTicket.CurrentStatus = status ?? "Pending";
                        UpdateTicket(foundTicket);

                        Console.WriteLine("");
                        foundTicket.PrintHeader();
                        Console.WriteLine(foundTicket.ToString());
                        SystemController.PromptContinue();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Status.");
                        SystemController.PromptContinue();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Ticket Id");
                    SystemController.PromptContinue();
                }
            }
            else
            {
                Console.WriteLine("Invalid Input");
                SystemController.PromptContinue();
            }
        }
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