using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Channel.ChannelCore.Business.Context;
using OOD_Project_Backend.Channel.ChannelCore.Business.Contracts;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Channel.ChannelCore.Controller;

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
        var result = await _channelService.AddChannelPicture(file, channelId);
        return result;
    }

    [HttpPut]
    [Route("Edit")]
    [Authorize]
    public async Task<Response> UpdateChannel([FromBody] ChannelUpdateRequest updateRequest)
    {
        return await _channelService.UpdateChannel(updateRequest);
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

    [HttpPost]
    [Route("AddAdmin")]
    [Authorize]
    public async Task<Response> AddAdminToChannel([FromBody] ChannelMembershipRequest channelMembershipRequest)
    {
        return await _channelMembershipService.AddAdminToChannel(channelMembershipRequest);
    }

    [HttpGet]
    [Route("AllChannels")]
    [Authorize]
    public async Task<Response> GetChannelsList()
    {
        return await _channelMembershipService.GetChannelsList();
    }

    [HttpPost]
    [Route("SetIncomeShare")]
    [Authorize]
    public async Task<Response> SetIncomeShares([FromBody] ChannelMembershipRequest channelMembershipRequest)
    {
        return await _channelMembershipService.SetIncomeShare(channelMembershipRequest);
    }

    [HttpGet]
    [Route("ShowAdmins/{channelId}")]
    [Authorize]
    public async Task<Response> ShowAdmins(int channelId)
    {
        return await _channelMembershipService.ShowAdmins(channelId);
    }

    [HttpDelete]
    [Route("Member")]
    [Authorize]
    public async Task<Response> RemoveMember(ChannelMembershipRequest membershipRequest)
    {
        return await _channelMembershipService.RemoveMember(membershipRequest);
    }

    [HttpDelete]
    [Route("Admin")]
    [Authorize]
    public async Task<Response> RemoveAdmin(ChannelMembershipRequest membershipRequest)
    {
        return await _channelMembershipService.RemoveAdmin(membershipRequest);
    }

    [HttpGet]
    [Route("GetRole/{channelId}")]
    [Authorize]
    public async Task<Response> GetUserRole(int channelId)
    {
        return await _channelMembershipService.GetUserRole(channelId);
    }
}