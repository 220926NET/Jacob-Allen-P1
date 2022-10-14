using Services;
using Models;

string message = "";

while (true)
{
    PrintMainMenu(message);
    message = GetMainMenuChoice();
}

static void PrintMainMenu(string message)
{
    Console.Clear();
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("              Reimbursement System               ");
    Console.WriteLine("-------------------------------------------------");
    Console.WriteLine("");
    Console.WriteLine("Please choose an option below:");
    Console.WriteLine("[1] Login existing user");
    Console.WriteLine("[2] Register new user");
    Console.WriteLine("[q] Quit");
    Console.WriteLine(message);

}

string GetMainMenuChoice()
{
    string? input = Console.ReadLine();

    if (input == null)
    {
        return "Please choose a valid input.";
    }
    else if (input == "1")
    {
        Console.WriteLine("Login Screen");
        User loginUser;
        bool loginSuccessful = false;
        loginSuccessful = Login(out loginUser);

        if (loginSuccessful) UserMenu(loginUser);
        else return "Login Failed";

        return "";
    }
    else if (input == "2")
    {
        Console.WriteLine("Register User");
        RegisterUser();
        return "";
    }
    else if (input == "3")
    {
        Console.WriteLine("Printing Users info");
        PrintUsersInfo();
        return "";
    }
    else if (input == "q")
    {
        Console.WriteLine("Goodbye!");
        Environment.Exit(0);
        return "";
    }
    else
    {
        return "Please choose a valid input.";
    }
}

void PrintUsersInfo()
{
    List<User> users = SystemController.GetAllUsers();

    foreach (User user in users)
    {
        Console.WriteLine(user.ToString());
    }
    Console.ReadLine();
}

void RegisterUser()
{
    List<User> users = SystemController.GetAllUsers();
    bool validInput = false;

    Console.WriteLine("Please enter a username that is 4-30 characters long.");

    string? username = "";

    while (!validInput)
    {
        Console.Write("Username: ");
        username = Console.ReadLine();

        username = username?.Trim();

        if (username == null)
        {
            Console.WriteLine("Please enter valid input.");
            continue;
        }
        else if (username.Length > 30)
        {
            Console.WriteLine("Username is too long. Please enter a username that is 4-30 characters long.");
            continue;
        }
        else if (username.Length < 4)
        {
            Console.WriteLine("Username is too short. Please enter a username that is 4-30 characters long.");
            continue;
        }

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
    }

    Console.WriteLine("Please enter a password that is 4-30 characters long.");

    validInput = false;
    string? password = "";

    while (!validInput)
    {
        Console.Write("Password: ");
        password = Console.ReadLine();

        password = password?.Trim();

        if (password == null)
        {
            Console.WriteLine("Please enter valid input.");
            continue;
        }
        else if (password.Length > 30)
        {
            Console.WriteLine("Password is too long. Please enter a username that is 4-30 characters long.");
            continue;
        }
        else if (password.Length < 4)
        {
            Console.WriteLine("Password is too short. Please enter a username that is 4-30 characters long.");
            continue;
        }

        validInput = true;
    }

    User newUser = new User(username, password);

    SystemController.AddUser(newUser);

}

bool Login(out User loginUser)
{
    string? username = "";
    string? password = "";
    bool loginSuccessful = false;

    Console.Write("Username: ");
    username = Console.ReadLine();
    Console.Write("Password: ");
    password = Console.ReadLine();
    loginUser = new User();

    List<User> users = SystemController.GetAllUsers();

    foreach (User user in users)
    {
        if (user.Username == username && user.Password == password)
        {
            Console.WriteLine($"Welcome {user.Username}!");
            Console.ReadLine();
            loginSuccessful = true;
            loginUser = user;
        }
    }

    if (!loginSuccessful)
    {
        Console.WriteLine("Invalid username or password");
        Console.ReadLine();
    }

    return loginSuccessful;

}

void UserMenu(User user)
{
    if (user.IsManager)
    {
        ManagerMenu(user);
    }
    else
    {
        EmployeeMenu(user);
    }
}

void ManagerMenu(User user)
{
Console.WriteLine($"Welcome Manager {user.Username}");
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

void EmployeeMenu(User user)
{
    Console.WriteLine($"Welcome Employee {user.Username}");
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
                AddTicket(user);
                Console.ReadLine();
                isValid = true;
                break;
            case "2":
                Console.WriteLine("View previous reports");
                List<Ticket> userTickets = new List<Ticket>();
                bool success = GetUserTickets(user, ref userTickets);

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