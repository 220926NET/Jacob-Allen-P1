using Models;
using DataAccess;

namespace Services;
public interface IDataAccessFactory
{
    public IDbAccess<User> GetUserDB();
    public ITicketDB GetTicketDB();
}