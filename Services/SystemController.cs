using Models;
using DataAccess;

namespace Services;

public static class SystemController
{
    public static void AddNewUser(User newUser)
    {
        new StaticStorage().AddUser(newUser);
    }

    public static bool UserExists(string username)
    {
        return new StaticStorage().UserExists(username);
    }

    public static List<User> GetAllUsers()
    {
        return new DBRepo().GetAllUsers();
    }
}