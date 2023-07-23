using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.Business.Abstractions;
using OOD_Project_Backend.Content.Business.Requests;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Common.Response;

namespace OOD_Project_Backend.Content.Controller;

[ApiController]
[Route("Content")]
public class ContentController : ControllerBase
{
    private readonly ContentService _contentService;

    public ContentController(ContentService contentService)
    {
        _contentService = contentService;
    }

    [HttpPost]
    [Route("Add")]
    [Authorize]
    public async Task<Response> AddContent([FromForm] IFormFile file,
        [FromForm] string Title,[FromForm] string Description,[FromForm] ContentType Type)
    {
        return await _contentService.Add(new ContentCreationRequest()
        {
            Type = Type,
            Description = Description,
            Title = Title,
            File = file,
            Value = ""
        });
    }

}