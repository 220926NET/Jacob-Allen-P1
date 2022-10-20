using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Expense_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

    [HttpGet]
    public ActionResult<List<User>> GetAllUsers()
    {
        List<User> users = SystemController.GetAllUsers();
        return Ok(users);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<List<User>> GetUser(int id)
    {
        List<User> users = SystemController.GetAllUsers();

        foreach(User user in users)
        {
            if(user.Id == id) return Ok(user);
        }
        return BadRequest("User not found");
    }
}
