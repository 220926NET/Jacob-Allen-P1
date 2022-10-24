using Models;
using DataAccess;

namespace Services;

public class TicketServices : ITicketServices
{
    private readonly IDbAccess<Ticket> _repo;

    public TicketServices(IDataAccessFactory dataAccessFactory)
    {
        _repo = dataAccessFactory.GetTicketDB();
    }
    public bool Add(ref Ticket ticket)
    {
        _repo.Add(ref ticket);
        return false;
    }

    public List<Ticket> GetAll()
    {
        return _repo.GetAll();
    } 
    public Ticket GetById(int id)
    {
        return _repo.GetById(id);
    }

    public bool GetPendingTickets(ref List<Ticket> tickets)
    {
        return new TicketDB(new SqlConnectionFactory()).GetPendingTickets(ref tickets);
    }
    public bool GetUserTickets(User user, ref List<Ticket> userTickets)
    {
        return new TicketDB(new SqlConnectionFactory()).GetUserTickets(user, ref userTickets);
    }

    public void UpdateTicket(Ticket ticket)
    {
        new TicketDB(new SqlConnectionFactory()).UpdateTicket(ticket);
    }

}