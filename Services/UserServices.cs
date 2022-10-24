using Models;
using DataAccess;

namespace Services;

public class UserServices : IUserServices
{
    private readonly IDbAccess<User> _repo;

    public UserServices(IDataAccessFactory dataAccessFactory)
    {
        _repo = dataAccessFactory.GetUserDB();
    }

    public bool Add(ref User newUser)
    {
        bool valid = CheckUserExists(newUser.Username);
        if (valid)
        {
            valid = _repo.Add(ref newUser);
        }

        return valid;
    }

    public List<User> GetAll()
    {
        return _repo.GetAll();
    }

    public User GetById(int id)
    {
        return _repo.GetById(id);
    }

    public bool LoginCheck(ref User loginUser)
    {
        List<User> users = GetAll();
        bool loginSuccessful = false;

        foreach (User user in users)
        {
            if (user.Username == loginUser.Username && user.Password == loginUser.Password)
            {
                loginSuccessful = true;
                loginUser = user;
            }
        }

        return loginSuccessful;
    }

    public bool CheckUserExists(string? username)
    {
        bool validInput = false;
        List<User> users = GetAll();

        if (username == null) return validInput;

        foreach (User user in users)
        {
            if (username == user.Username)
            {
                validInput = false;
                break;
            }

            validInput = true;
        }
        return validInput;
    }
}