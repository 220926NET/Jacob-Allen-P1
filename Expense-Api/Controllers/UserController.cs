using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Expense_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserServices _service;

    public UsersController(IUserServices service)
    {
        _service = service;
    }


    [HttpGet]
    public ActionResult<List<User>> GetAllUsers()
    {
        List<User> users = _service.GetAll();
        return Ok(users);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        User user = _service.GetById(id);

        if(user.Id == id) return Ok(user);

        return BadRequest("User not found");
    }

    [HttpPost]
    [Route("Register")]
    public ActionResult<User> AddUser(User user)
    {
        bool success = _service.Add(ref user);
        return success ? Created("User Registered", user) : BadRequest("Username already exists");
    }

    [HttpPost]
    [Route("login")]
    public ActionResult<User> LoginUser(User loginUser)
    {

        bool success = _service.LoginCheck(ref loginUser);
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
