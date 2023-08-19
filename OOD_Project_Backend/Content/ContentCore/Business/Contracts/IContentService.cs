using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.ContentCore.Business.Contexts;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.ContentCore.Business.Contracts;

public interface IContentService
{
    Task<Response> Add(ContentCreationRequest request);
    Task<Response> GetChannelContentsMetadata(int channelId);
    Task<Response> Show(int contentId);
    Task<Response> Delete(int contentId);
    Task<Response> Update(ContentUpdateRequest updateRequest);
    Task<Response> AddInteraction(int contentId);
    Task<Response> DeleteInteraction(int contentId);
    Task<Response> GetContentMetadata(int contentId);
}