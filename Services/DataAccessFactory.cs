using Models;
using DataAccess;

namespace Services;

public class DataAccessFactory : IDataAccessFactory
{
    private readonly SqlConnectionFactory _connection = new SqlConnectionFactory();

    public IDbAccess<User> GetUserDB()
    {
        return new UserDB(_connection);
    }

    public ITicketDB GetTicketDB()
    {
        return new TicketDB(_connection);
    }
}