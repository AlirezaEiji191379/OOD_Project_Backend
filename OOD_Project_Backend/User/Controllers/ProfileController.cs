using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.User.Business.Contracts;
using OOD_Project_Backend.User.Business.Requests;

namespace OOD_Project_Backend.User.Controllers;

[ApiController]
[Route("Profile")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;
    private readonly IAuthenticationService _authenticationService;

    public ProfileController(IProfileService profileService, IAuthenticationService authenticationService)
    {
        _profileService = profileService;
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("AddProfile")]
    [Authorize]
    public async Task<Response> AddProfile([FromBody] ProfileRequest profileRequest)
    {
        var userId = _authenticationService.GetCurrentUserId(HttpContext);
        var result = await _profileService.Add(profileRequest,userId);
        return result;
    }

    [HttpPost]
    [Route("AddProfilePic")]
    [Authorize]
    public async Task<Response> AddProfilePic([FromForm] IFormFile file)
    {
        var userId = _authenticationService.GetCurrentUserId(HttpContext);
        var result = await _profileService.AddProfilePicture(file,userId);
        return result;
    }

    [HttpGet]
    [Route("GetProfile")]
    [Authorize]
    public async Task<Response> ShowProfile()
    {
        var userId = _authenticationService.GetCurrentUserId(HttpContext);
        var result = await _profileService.ShowProfile(userId);
        return result;
    }
}