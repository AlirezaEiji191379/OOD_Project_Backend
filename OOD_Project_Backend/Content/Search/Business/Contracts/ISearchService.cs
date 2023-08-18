using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.Search.Business.Contracts;

public interface ISearchService
{
    Task<Response> SearchInChannel(int channelId,string name);
    Task<Response> SearchInSystem(string name);
}