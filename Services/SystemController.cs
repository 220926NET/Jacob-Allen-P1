using Models;
using DataAccess;

namespace Services;

static class SystemController
{
    public static void AddNewUser(User newUser)
    {
        new StaticStorage().AddUser(newUser);
    }

    public static bool UserExists(string username)
    {
        return new StaticStorage().UserExists(username);
    }
}