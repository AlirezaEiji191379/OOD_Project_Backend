using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Channel.Business.Abstractions;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Common.Response;

namespace OOD_Project_Backend.Channel.Controller;

[ApiController]
[Route("Channel")]
public class ChannelController : ControllerBase
{
    private readonly ChannelService _channelService;

    public ChannelController(ChannelService channelService)
    {
        _channelService = channelService;
    }

    [HttpPost]
    [Route("{name}")]
    [Authorize]
    public async Task<Response> CreateChannel(string name)
    {
        return await _channelService.CreateChannel(name,(int)HttpContext.Items["User"]);
    }

    [HttpGet]
    [Route("getAll")]
    [Authorize]
    public async Task<Response> GetAllChannels()
    {
        return await _channelService.GetAllUsersChannels((int)HttpContext.Items["User"]);
    }

}