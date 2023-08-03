using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Channel.Business.Context;
using OOD_Project_Backend.Channel.Business.Contracts;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.Channel.Controller;

[ApiController]
[Route("Channel")]
public class ChannelController : ControllerBase
{
    private readonly IChannelService _channelService;
    private readonly IUserFacade _userFacade;

    public ChannelController(IChannelService channelService,
        IUserFacade userFacade)
    {
        _channelService = channelService;
        _userFacade = userFacade;
    }

    [HttpPost]
    [Route("Add")]
    [Authorize]
    public async Task<Response> CreateChannel([FromBody] ChannelCreateRequest channelCreateRequest)
    {
        var userId = _userFacade.GetCurrentUserId(HttpContext);
        return await _channelService.CreateChannel(channelCreateRequest.ChannelName, userId);
    }

    [HttpGet]
    [Route("getAll")]
    [Authorize]
    public async Task<Response> GetAllChannels()
    {
        var userId = _userFacade.GetCurrentUserId(HttpContext);
        return await _channelService.GetAllUsersChannels(userId);
    }
}