using Models;

namespace Services;

public interface IUserServices : IService<User>
{
    bool LoginCheck(ref User user);
    bool CheckUserExists(string username);
}