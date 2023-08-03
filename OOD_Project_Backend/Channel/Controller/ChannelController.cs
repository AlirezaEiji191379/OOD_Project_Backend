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
    private readonly IChannelMembershipService _channelMembershipService;

    public ChannelController(IChannelService channelService,
        IUserFacade userFacade,
        IChannelMembershipService channelMembershipService)
    {
        _channelService = channelService;
        _userFacade = userFacade;
        _channelMembershipService = channelMembershipService;
    }

    [HttpPost]
    [Route("Add")]
    [Authorize]
    public async Task<Response> CreateChannel([FromBody] ChannelCreateRequest channelCreateRequest)
    {
        var userId = _userFacade.GetCurrentUserId(HttpContext);
        return await _channelService.CreateChannel(channelCreateRequest.ChannelName, userId);
    }

    [HttpPost]
    [Route("AddPicture/{channelId}")]
    [Authorize]
    public async Task<Response> AddChannelPhoto([FromForm] IFormFile file, int channelId)
    {
        var userId = _userFacade.GetCurrentUserId(HttpContext);
        var result = await _channelService.AddChannelPicture(file, userId, channelId);
        return result;
    }

    [HttpPost]
    [Route("Join/{joinLink}")]
    [Authorize]
    public async Task<Response> JoinChannel(string joinLink)
    {
        var userId = _userFacade.GetCurrentUserId(HttpContext);
        var result = await _channelMembershipService.JoinChannel(joinLink, userId);
        return result;
    }

    [HttpGet]
    [Route("GetJoinedChannels")]
    [Authorize]
    public async Task<Response> GetAllChannels()
    {
        var userId = _userFacade.GetCurrentUserId(HttpContext);
        return await _channelService.ShowChannelsList(userId);
    }

    [HttpGet]
    [Route("GetChannelMembers/{channelId}")]
    [Authorize]
    public async Task<Response> GetChannelMembers(int channelId)
    {
        return await _channelMembershipService.ShowMembers(channelId);
    }

}