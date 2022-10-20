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

        switch (input)
        {
        case null:
            return "Please choose a valid input.";

        case "1":
            Console.Clear();
            Console.WriteLine("Login Screen");

            User loginUser;
            bool loginSuccessful = Login(out loginUser);

            if (loginSuccessful) UserMenu(loginUser);
            
            return "";

        case "2":
            Console.Clear();
            Console.WriteLine("Register User");
            RegisterUser();
            return "";

        case "q":
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
            return "";

        default:
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

        loginSuccessful = SystemController.LoginCheck(username, password, ref loginUser);

        if (!loginSuccessful)
        {
            Console.WriteLine("Invalid username or password");
            SystemController.PromptContinue();
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

    static void RegisterUser()
    {
        bool validInput = false;

        Console.WriteLine("Please enter a username that is 4-12 characters long.");

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
            else if (username.Length > 12)
            {
                Console.WriteLine("Username is too long. Please enter a username that is 4-12 characters long.");
                continue;
            }
            else if (username.Length < 4)
            {
                Console.WriteLine("Username is too short. Please enter a username that is 4-12 characters long.");
                continue;
            }
            else
            {
                validInput = SystemController.CheckUserExists(username);
                if (!validInput) Console.WriteLine("Username already exists, please enter a new one.");
            }
        }

        Console.WriteLine("Please enter a password that is 4-12 characters long.");

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
            else if (password.Length > 12)
            {
                Console.WriteLine("Password is too long. Please enter a username that is 4-12 characters long.");
                continue;
            }
            else if (password.Length < 4)
            {
                Console.WriteLine("Password is too short. Please enter a username that is 4-12 characters long.");
                continue;
            }

            validInput = true;
        }

        User newUser = new User(username, password);

        SystemController.AddUser(newUser);
        Console.WriteLine($"\nUser {newUser.Username} successfully registered!");
        SystemController.PromptContinue();
    }
}