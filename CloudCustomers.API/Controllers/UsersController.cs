using CloudCustomers.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UsersController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAllUsers();
        if(users.Any())
            return Ok(users);
        return NotFound();
    }

    [HttpGet("{id}", Name = "GetUserProfile")]
    public async Task<IActionResult> GetUserProfile(int id)
    {
        var user = await _userService.GetUserProfile(id);
        if(user != null)
            return Ok(user);
        return NotFound();
    }

    [HttpPost(Name = "AuthenticateUser")]
    public async Task<IActionResult> AuthenticateUser(string email, string passWord)
    {
        var user = await _authService.AuthenticateUser(email, passWord);
        if(user != null)
            return Ok(user);
        return NotFound();
    }
}
