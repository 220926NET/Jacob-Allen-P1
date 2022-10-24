using Models;

namespace Services;

public interface ITicketServices : IService<Ticket>
{
    bool GetPendingTickets(ref List<Ticket> tickets);
    bool GetUserTickets(User user, ref List<Ticket> userTickets);
    void UpdateTicket(Ticket ticket);
}