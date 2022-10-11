using Services;
using Models;

PrintMainMenu();
GetMainMenuChoice();

static void PrintMainMenu()
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

}

static void GetMainMenuChoice()
{
    string? input = Console.ReadLine();

    if (input == null)
    {
        PrintMainMenu();
        Console.WriteLine("Please choose a valid input.");
        GetMainMenuChoice();
    }
    else if (input == "1")
    {
        Console.WriteLine("Login Screen");
    }
    else if (input == "2")
    {
        Console.WriteLine("Register User");
    }
    else if (input == "3")
    {
        Console.WriteLine("Printing Users info");
        PrintUsersInfo();
    }
    else if (input == "q")
    {
        Console.WriteLine("Goodbye!");
    }
    else
    {
        PrintMainMenu();
        Console.WriteLine("Please choose a valid input.");
        GetMainMenuChoice();
    }

    void PrintUsersInfo()
    {
        List<User> users = SystemController.GetAllUsers();

        foreach (User user in users)
        {
            Console.WriteLine(user.ToString());
        }
    }
}