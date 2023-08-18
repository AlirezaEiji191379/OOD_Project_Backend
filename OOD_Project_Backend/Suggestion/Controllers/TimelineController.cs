using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Suggestion.Facade.Abstractions;

namespace OOD_Project_Backend.Suggestion.Controllers;

[ApiController]
[Route("Timeline")]
public class TimelineController : ControllerBase
{
    private readonly ITimelineService _timelineService;

    public TimelineController(ITimelineService timelineService)
    {
        _timelineService = timelineService;
    }

    [HttpGet]
    [Route("Load")]
    [Authorize]
    public async Task<Response> Load()
    {
        return await _timelineService.Load();
    }

}