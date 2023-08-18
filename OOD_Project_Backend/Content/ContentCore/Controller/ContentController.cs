using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.ContentCore.Business.Contexts;
using OOD_Project_Backend.Content.ContentCore.Business.Contracts;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.ContentCore.Controller;

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
    [Route("GetContentsMetaData/{channelId}")]
    [Authorize]
    public async Task<Response> GetChannelContentMetadata(int channelId)
    {
         return await _contentService.GetChannelContentsMetadata(channelId);
    }

    [HttpGet]
    [Route("ShowContent/{contentId}")]
    [Authorize]
    public async Task<Response> ShowContent(int contentId)
    {
        return await _contentService.Show(contentId);
    }

    [HttpPut]
    [Route("")]
    [Authorize]
    public async Task<Response> UpdateContent([FromForm] ContentUpdateRequest contentUpdateRequest)
    {
        return await _contentService.Update(contentUpdateRequest);
    }

    [HttpDelete]
    [Route("RemoveContent/{contentId}")]
    [Authorize]
    public async Task<Response> DeleteContent(int contentId)
    {
        return await _contentService.Delete(contentId);
    }

}