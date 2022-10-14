using Models;
using Services;

public class EmployeeMenu
{
    User employee;

    public EmployeeMenu(User user)
    {
        employee = user;
        StartMenu();
    }

    void StartMenu()
    {
        Console.WriteLine($"Welcome Employee {employee.Username}");
        Console.WriteLine("Please choose an option below:");
        Console.WriteLine("[1] Add new expense report");
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
                    Console.WriteLine("Add new expense report");
                    AddTicket(employee);
                    Console.ReadLine();
                    isValid = true;
                    break;
                case "2":
                    Console.WriteLine("View previous reports");
                    List<Ticket> userTickets = new List<Ticket>();
                    bool success = GetUserTickets(employee, ref userTickets);

                    if (success)
                    {
                        foreach (Ticket ticket in userTickets)
                        {
                            Console.WriteLine(ticket.ToString());
                        }
                    }
                    else Console.WriteLine("You have no tickets.");

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

    void AddTicket(User user)
    {
        string? description;
        decimal amount = 0.0m;
        bool validInput = false;
        Ticket ticket;

        Console.WriteLine("Enter Description");
        description = Console.ReadLine();

        while(!validInput && amount <= 0.0m)
        {
            Console.WriteLine("Enter Amount");
            validInput = Decimal.TryParse(Console.ReadLine(), out amount);
        }


        if (description != null)
        {
            ticket = new Ticket(user.Id, description, amount);
            SystemController.AddTicket(user, ref ticket);
            Console.WriteLine(ticket.ToString());
        }
    }

    bool GetUserTickets(User user, ref List<Ticket> userTickets)
    {
        return SystemController.GetUserTickets(user, ref userTickets);
    }
}