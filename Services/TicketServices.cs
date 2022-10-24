using Models;
using DataAccess;

namespace Services;

public class TicketServices : ITicketServices
{
    private readonly ITicketDB _repo;

    public TicketServices(IDataAccessFactory dataAccessFactory)
    {
        _repo = dataAccessFactory.GetTicketDB();
    }

    public bool Add(ref Ticket ticket)
    {
        return _repo.Add(ref ticket);
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
        return _repo.GetPendingTickets(ref tickets);
    }

    public bool GetUserTickets(User user, ref List<Ticket> userTickets)
    {
        return _repo.GetUserTickets(user, ref userTickets);
    }

    public void UpdateTicket(Ticket ticket)
    {
        _repo.UpdateTicket(ticket);
    }

}