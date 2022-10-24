using Models;

namespace Services;

public interface ITicketServices : IService<Ticket>
{
    /// <summary>
    /// Gets all Tickets with Status = "Pending"
    /// </summary>
    /// <param name="tickets"></param>
    /// <returns></returns>
    bool GetPendingTickets(ref List<Ticket> tickets);
    bool GetUserTickets(User user, ref List<Ticket> userTickets);
    void UpdateTicket(Ticket ticket);
}