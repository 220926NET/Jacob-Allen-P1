using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Expense_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{

    [HttpGet]
    public ActionResult<List<Ticket>> GetAllTickets()
    {
        List<Ticket> tickets = new List<Ticket>();
        bool success = SystemController.GetAllTickets(ref tickets);

        return success ? Ok(tickets) : BadRequest();
    }

    [HttpGet]
    [Route("{userId}")]
    public ActionResult<List<Ticket>> GetUserTickets(int userId)
    {
        int id = int.Parse(HttpContext.Request.Headers["UserId"]);

        if (id == userId)
        {
            User user = SystemController.GetUser(userId);
            List<Ticket> tickets = new List<Ticket>();
            SystemController.GetUserTickets(user, ref tickets);
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
        bool success = SystemController.GetPendingTickets(ref tickets);

        return success ? Ok(tickets) : BadRequest();
    }

    // TODO Refactor all this
    [HttpPost]
    [Route("Submit")]
    public ActionResult<Ticket> AddTicket(Ticket ticket)
    {
        string idString = Request.Headers["UserId"];
        List<User> users = SystemController.GetAllUsers();
        int id = int.Parse(idString);
        User currentUser = new User();
        foreach(User user in users)
        {
            if (id == user.Id)
            {
                currentUser = user;
                break;
            }
        }

        SystemController.AddTicket(currentUser, ref ticket);
        return Ok(ticket);
    }

    [HttpPut]
    [Route("{ticketId}")]
    public ActionResult<Ticket> UpdateTicket(int ticketId, [FromForm] string status)
    {
        User user = SystemController.GetUser(int.Parse(Request.Headers["UserId"]));
        if (!user.IsManager) return StatusCode(403, "Unauthorized");

        Ticket ticket = new Ticket();
        SystemController.GetTicket(ticketId, ref ticket);

        ticket.CurrentStatus = status;
        SystemController.UpdateTicket(ticket);
        return Ok(ticket);
    }
}
