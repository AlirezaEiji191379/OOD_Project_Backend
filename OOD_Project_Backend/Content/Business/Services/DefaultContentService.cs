using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.Business.Contexts;
using OOD_Project_Backend.Content.Business.Contracts;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Content.Business.Services;

public class DefaultContentService : IContentService
{
    private readonly IContentRepository _contentRepository;
    private readonly IContentMetaDataRepository _contentMetadataRepository;
    public DefaultContentService(IContentRepository contentRepository, IContentMetaDataRepository contentMetadataRepository)
    {
        _contentRepository = contentRepository;
        _contentMetadataRepository = contentMetadataRepository;
    }

    public async Task<Response> Add(ContentCreationRequest request)
    {
        return null;
    }

    public async Task<Response> GetChannelContentsMetadata(int channelId)
    {
        try
        {
            var contentDtos = await _contentRepository.GetChannelContents(channelId);
            return new Response(200,new {Message = contentDtos});
        }
        catch (Exception e)
        {
            return new Response(400,"could not returve");
        }
    }
}