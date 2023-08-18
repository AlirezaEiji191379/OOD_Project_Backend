using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.Search.Business.Contracts;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.ContentCore.Controller;

[ApiController]
[Route("Search")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }


    [HttpGet]
    [Route("{channelId}")]
    [Authorize]
    public async Task<Response> SearchChannel([FromQuery] string title, int channelId)
    {
        return await _searchService.SearchInChannel(channelId, title);
    }
    
    [HttpGet]
    [Authorize]
    public async Task<Response> SearchSystem([FromQuery] string title)
    {
        return await _searchService.SearchInSystem(title);
    }
    
}