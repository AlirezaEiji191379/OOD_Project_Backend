using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Suggestion.Facade.Abstractions;

namespace OOD_Project_Backend.Suggestion.Business;

public class TimelineService : ITimelineService
{
    private readonly IContentMetaDataRepository _contentMetaDataRepository;

    public TimelineService(IContentMetaDataRepository contentMetaDataRepository)
    {
        _contentMetaDataRepository = contentMetaDataRepository;
    }


    public async Task<Response> Load()
    {
        try
        {
            var contents = await _contentMetaDataRepository.FindRandom();
            return new Response(200, new { Message = contents });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "failed to load timeline!" });
        }
    }
}