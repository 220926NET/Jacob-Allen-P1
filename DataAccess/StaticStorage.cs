using Models;

namespace DataAccess;
public class StaticStorage
{
    private static List<User> allUsers = new List<User> {
        new User {
            Username = "admin",
            Password = "pass"
        },
        new User {
            Username = "jallen",
            Password = "1234"
        },
        new User {
            Username = "test",
            Password = "test",
        }
    };

    public List<User> GetUsers()
    {
        return allUsers;
    }

    public void AddUser(User newUser)
    {
        allUsers.Add(newUser);
    }

    public bool UserExists(string username)
    {
        return false;
    }

}
