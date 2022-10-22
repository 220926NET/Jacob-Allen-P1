using Models;
using DataAccess;

namespace Services;

public class DataAccessFactory : IDataAccessFactory
{
    public IDbAccess<User> GetUserDB()
    {
        return new UserDB(new SqlConnectionFactory());
    }

    // public IDbAccess<Ticket> GetTicketDB()
    // {
    //     return new TicketDB(new SqlConnectionFactory());
    // }
}