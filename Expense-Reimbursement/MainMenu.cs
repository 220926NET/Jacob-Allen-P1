using Models;
using Services;

namespace UI;

public static class MainMenu
{
    public static void PrintMainMenu(string message)
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

    public static string GetMainMenuChoice()
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

    static bool Login(out User loginUser)
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

    static void UserMenu(User user)
    {
        if (user.IsManager)
        {
            new ManagerMenu(user);
        }
        else
        {
            new EmployeeMenu(user);
        }
    }
    
    static void PrintUsersInfo()
    {
        List<User> users = SystemController.GetAllUsers();

        foreach (User user in users)
        {
            Console.WriteLine(user.ToString());
        }
        Console.ReadLine();
    }

    static void RegisterUser()
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
}