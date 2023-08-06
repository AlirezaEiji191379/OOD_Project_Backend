using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.ContentCore.Business.Contexts;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.ContentCore.Business.Contracts;

public interface IContentService
{
    Task<Response> Add(ContentCreationRequest request);
    Task<Response> GetChannelContentsMetadata(int channelId);
    Task<FileResult> Show(int contentId);
    Task<Response> Delete(int contentId);
}