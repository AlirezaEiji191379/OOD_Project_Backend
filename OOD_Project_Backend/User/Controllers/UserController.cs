using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Core.Common.Response;
using OOD_Project_Backend.User.Business.Abstractions;
using OOD_Project_Backend.User.Business.Requests;

namespace OOD_Project_Backend.User.Controllers;

[ApiController]
[Route("User")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
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
    
}