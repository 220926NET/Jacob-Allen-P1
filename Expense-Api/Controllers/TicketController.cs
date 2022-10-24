using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Expense_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketServices _ticketService;
    private readonly IUserServices _userService;

    public TicketsController(ITicketServices ticketService, IUserServices userService)
    {
        _ticketService = ticketService;
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<List<Ticket>> GetAllTickets()
    {
        List<Ticket> tickets = _ticketService.GetAll();

        return Ok(tickets);
    }

    [HttpGet]
    [Route("{userId}")]
    public ActionResult<List<Ticket>> GetUserTickets(int userId)
    {
        int id = int.Parse(HttpContext.Request.Headers["UserId"]);

        if (id == userId)
        {
            User user = _userService.GetById(userId);
            List<Ticket> tickets = new List<Ticket>();
            _ticketService.GetUserTickets(user, ref tickets);
            return Ok(tickets);
        }
        else 
        {
            return StatusCode(403, "Forbidden Access");
        }
        
    }

    [HttpGet]
    [Route("Pending")]
    public ActionResult<List<Ticket>> GetPendingTickets()
    {
        List<Ticket> tickets = new List<Ticket>();
        bool success = _ticketService.GetPendingTickets(ref tickets);

        return success ? Ok(tickets) : BadRequest();
    }

    // TODO Refactor all this
    [HttpPost]
    [Route("Submit")]
    public ActionResult<Ticket> AddTicket(Ticket ticket)
    {
        int id = int.Parse(Request.Headers["UserId"]);
        ticket.UserId = id;

        _ticketService.Add(ref ticket);
        return Ok(ticket);
    }

    [HttpPut]
    [Route("{ticketId}")]
    public ActionResult<Ticket> UpdateTicket(int ticketId, [FromForm] string status)
    {
        User user = _userService.GetById(int.Parse(Request.Headers["UserId"]));
        if (!user.IsManager) return StatusCode(403, "Unauthorized");

        Ticket ticket =  _ticketService.GetById(ticketId);

        ticket.CurrentStatus = status;
        _ticketService.UpdateTicket(ticket);
        return Ok(ticket);
    }
}
