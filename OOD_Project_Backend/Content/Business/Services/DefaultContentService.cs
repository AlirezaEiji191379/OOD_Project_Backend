using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.Business.Contexts;
using OOD_Project_Backend.Content.Business.Contracts;
using OOD_Project_Backend.Content.Business.Creation;
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
    private readonly IContentCreation _contentCreation;
    
    public DefaultContentService(IContentRepository contentRepository,
        IContentMetaDataRepository contentMetadataRepository,
        IContentCreation contentCreation)
    {
        _contentRepository = contentRepository;
        _contentMetadataRepository = contentMetadataRepository;
        _contentCreation = contentCreation;
    }

    public async Task<Response> Add(ContentCreationRequest request)
    {
        try
        {
            await _contentCreation.Generate(request);
            return new Response(201,new {Message = "Created"});
        }
        catch (Exception e)
        {
            return new Response(400,new {Message = "add content failed!"});
        }
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