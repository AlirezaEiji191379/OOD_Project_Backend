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
    private readonly IChannelMembershipService _channelMembershipService;

    public ChannelController(IChannelService channelService,
        IChannelMembershipService channelMembershipService)
    {
        _channelService = channelService;
        _channelMembershipService = channelMembershipService;
    }

    [HttpPost]
    [Route("Add")]
    [Authorize]
    public async Task<Response> CreateChannel([FromBody] ChannelCreateRequest channelCreateRequest)
    {
        return await _channelService.CreateChannel(channelCreateRequest.ChannelName);
    }

    [HttpPost]
    [Route("AddPicture/{channelId}")]
    [Authorize]
    public async Task<Response> AddChannelPhoto([FromForm] IFormFile file, int channelId)
    {
        var result = await _channelService.AddChannelPicture(file,channelId);
        return result;
    }

    [HttpPost]
    [Route("Join/{joinLink}")]
    [Authorize]
    public async Task<Response> JoinChannel(string joinLink)
    {
        var result = await _channelMembershipService.JoinChannel(joinLink);
        return result;
    }

    [HttpGet]
    [Route("GetJoinedChannels")]
    [Authorize]
    public async Task<Response> GetAllChannels()
    {
        return await _channelService.ShowChannelsList();
    }

    [HttpGet]
    [Route("GetChannelMembers/{channelId}")]
    [Authorize]
    public async Task<Response> GetChannelMembers(int channelId)
    {
        return await _channelMembershipService.ShowMembers(channelId);
    }

}