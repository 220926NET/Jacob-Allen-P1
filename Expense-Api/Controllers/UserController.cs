using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Expense_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{

    [HttpGet]
    public ActionResult<List<User>> GetAllUsers()
    {
        List<User> users = SystemController.GetAllUsers();
        return Ok(users);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        List<User> users = SystemController.GetAllUsers();

        foreach(User user in users)
        {
            if(user.Id == id) return Ok(user);
        }
        return BadRequest("User not found");
    }

    [HttpPost]
    [Route("Register")]
    public ActionResult<User> AddUser(User user)
    {
        bool success = SystemController.AddUser(user);
        return success ? Ok(user) : BadRequest("Username already exists");
    }

    [HttpPost]
    [Route("login")]
    public ActionResult<User> LoginUser(User loginUser)
    {

        bool success = SystemController.LoginCheck(ref loginUser);
        // string output;
        // output = Request.Headers["UserId"];

        if (success)
        {
            HttpContext.Response.Headers.Add("UserId", loginUser.Id.ToString());
            return loginUser.IsManager ? Ok("Welcome Manager") : Ok($"Welcome Employee");
        }

        return BadRequest();
    }
}
