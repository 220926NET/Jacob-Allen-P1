using Models;

namespace DataAccess;

public interface ITicketDB : IDbAccess<Ticket>
{
    bool GetUserTickets(User user, ref List<Ticket> userTickets);
    bool GetPendingTickets(ref List<Ticket> pendingTickets);
    void UpdateTicket(Ticket ticket);
}