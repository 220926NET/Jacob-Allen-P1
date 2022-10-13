using Models;

namespace DataAccess;

public interface IUserAccess
{
    List<User> GetAllUsers();

    void AddUser();
}