using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Content.Search.Business.Contracts;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.Search.Business.Services;

public class SearchService : ISearchService
{
    private readonly IContentMetaDataRepository _contentMetaDataRepository;

    public SearchService(IContentMetaDataRepository contentMetaDataRepository)
    {
        _contentMetaDataRepository = contentMetaDataRepository;
    }


    public async Task<Response> SearchInChannel(int channelId, string name)
    {
        try
        {
            var contentDtos =
                await _contentMetaDataRepository.FindByChannelIdIncludeContentAndLikeTitle(channelId, name);
            return new Response(200,new {Message = contentDtos});
        }
        catch (Exception exception)
        {
            return new Response(404, new { Message = "search failed!" });
        }
    }

    public async Task<Response> SearchInSystem(string name)
    {
        try
        {
            var contentDtos =
                await _contentMetaDataRepository.FindByLikeTitle(name);
            return new Response(200,new {Message = contentDtos});
        }
        catch (Exception exception)
        {
            return new Response(404, new { Message = "search failed!" });
        }
    }
}