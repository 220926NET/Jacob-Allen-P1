using Models;
using UI;
using Services;

string message = "";

while (true)
{
    MainMenu.PrintMainMenu(message);
    message = MainMenu.GetMainMenuChoice();
}
