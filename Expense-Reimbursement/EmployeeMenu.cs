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
        bool exit = false;

        while(!exit)
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"             Welcome Employee {employee.Username}");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Please choose an option below:");
            Console.WriteLine("[1] Add new expense report");
            Console.WriteLine("[2] View previous expense reports");
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
                        Console.WriteLine("Add new expense report");
                        AddTicket(employee);
                        SystemController.PromptContinue();
                        isValid = true;
                        break;
                    case "2":
                        Console.Clear();
                        
                        List<Ticket> userTickets = new List<Ticket>();
                        bool success = GetUserTickets(employee, ref userTickets);

                        if (success)
                        {
                            userTickets[0].PrintHeader();
                            foreach (Ticket ticket in userTickets)
                            {
                                Console.WriteLine(ticket.ToString());
                            }
                        }
                        else Console.WriteLine("You have no tickets.");

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
            ticket.PrintHeader();
            Console.WriteLine(ticket.ToString());
        }
    }

    bool GetUserTickets(User user, ref List<Ticket> userTickets)
    {
        return SystemController.GetUserTickets(user, ref userTickets);
    }
}