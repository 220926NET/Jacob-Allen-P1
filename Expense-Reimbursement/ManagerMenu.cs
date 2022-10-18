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
            Console.WriteLine($"Welcome Manager {manager.Username}");
            Console.WriteLine("Please choose an option below:");
            Console.WriteLine("[1] Update expense reports");
            Console.WriteLine("[2] View previous expense reports");
            Console.WriteLine("[3] Print all users");
            Console.WriteLine("[q] Logout");


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
                                if (status == "Approved" || status == "Denied")
                                {
                                    ticket.CurrentStatus = status ?? "Pending";
                                    UpdateTicket(ticket);
                                    Console.WriteLine(ticket.ToString());
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Status.");
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadLine();
                                }
                            }
                        }
                        isValid = true;
                        break;
                    case "2":
                        Console.Clear();
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
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Printing Users info");
                        PrintUsersInfo();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        isValid = true;
                        break;
                    case "q":
                        Console.WriteLine("Logging Out");
                        Console.ReadLine();
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

    static void PrintUsersInfo()
    {
        List<User> users = SystemController.GetAllUsers();
        Console.WriteLine(String.Format("|{0,-5}|{1,-15}|{2,-10}|","ID","Username","IsManager"));
        Console.WriteLine(new String('-', 34));

        foreach (User user in users)
        {
            Console.WriteLine(user.ToString());
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