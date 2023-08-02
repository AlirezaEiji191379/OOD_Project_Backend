using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.User.Business.Contracts;
using OOD_Project_Backend.User.Business.Requests;

namespace OOD_Project_Backend.User.Controllers;

[ApiController]
[Route("User")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("SignUp")]
    public async Task<Response> Register([FromBody] RegisterRequest registerRequest)
    {
        var result = await _userService.Register(registerRequest);
        return result;
    }

    [HttpPost]
    [Route("SignIn")]
    public async Task<Response> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _userService.Login(loginRequest);
        return result;
    }

    [HttpDelete]
    [Route("Logout")]
    [Authorize]
    public async Task<Response> Logout()
    {
        var result = await _userService.Logout(HttpContext);
        return result;
    }
    
    [HttpGet]
    [Route("Test")]
    [Authorize]
    public IActionResult Test()
    {
        return Ok("hi");
    }
    
}