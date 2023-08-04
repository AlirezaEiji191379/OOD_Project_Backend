using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.Business.Contexts;
using OOD_Project_Backend.Content.Business.Contracts;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.Controller;

[ApiController]
[Route("Content")]
public class ContentController : ControllerBase
{
    private readonly IContentService _contentService;

    public ContentController(IContentService contentService)
    {
        _contentService = contentService;
    }

    [HttpPost]
    [Route("Add")]
    [Authorize]
    public async Task<Response> AddContent([FromForm] ContentCreationRequest creationRequest )
    {
        return await _contentService.Add(creationRequest);
    }

    [HttpGet]
    [Route("GetAll/{channelId}")]
    [Authorize]
    public async Task<Response> GetContnetsOfChannels(int channelId)
    {
        return await _contentService.GetChannelContentsMetadata(channelId);
    }

}