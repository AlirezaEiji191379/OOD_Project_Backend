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
    private readonly IAuthenticator _jwtAuth;

    public ProfileController(IProfileService profileService, IAuthenticationService authenticationService, IAuthenticator jwtAuth)
    {
        _profileService = profileService;
        _authenticationService = authenticationService;
        _jwtAuth = jwtAuth;
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

    [HttpDelete]
    [Route("RemoveProfile")]
    [Authorize]
    public async Task<Response> DeleteProfiles()
    {
        var userId = _authenticationService.GetCurrentUserId(HttpContext);
        return await _profileService.RequestDeleteProfile(userId);
    }

    [HttpDelete]
    [Route("VerifyRemoveProfile/{code}")]
    [Authorize]
    public async Task<Response> VerifyRemoveProfile(int code)
    {
        var userId = _authenticationService.GetCurrentUserId(HttpContext);
        var tokenId = _jwtAuth.FindJwtId(HttpContext.Request.Headers["X-Auth-Token"].FirstOrDefault());
        return await _profileService.VerifyDeleteProfile(userId,code,tokenId);
    }

}