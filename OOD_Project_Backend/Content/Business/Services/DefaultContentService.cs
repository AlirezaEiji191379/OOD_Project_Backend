using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.Business.Abstractions;
using OOD_Project_Backend.Content.Business.Requests;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository;
using OOD_Project_Backend.Content.DataAccess.Repository.Abstractions;
using OOD_Project_Backend.Core.Common.Response;
using OOD_Project_Backend.Core.DataAccess.Abstractions;

namespace OOD_Project_Backend.Content.Business.Services;

public class DefaultContentService : ContentService
{
    private readonly IContentRepository _contentRepository;
    private readonly IBaseRepository<ContentMetaDataEntity> _contentMetadataRepository;
    public DefaultContentService(IContentRepository contentRepository, IBaseRepository<ContentMetaDataEntity> contentMetadataRepository)
    {
        _contentRepository = contentRepository;
        _contentMetadataRepository = contentMetadataRepository;
    }

    public async Task<Response> Add(ContentCreationRequest request)
    {
        try
        {
            var content = new ContentEntity()
            {
                Description = request.Description,
                Title = request.Title,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                ChannelId = request.ChannelId
            };
            var contentMetaData = new ContentMetaDataEntity()
            {
                ContentType = request.Type,
                Premium = false,
                Price = 0,
                Content = content,
                FileName = request.File.FileName
            };
            var fileEntity = new FileEntity()
            {
                Quality = FileQuality._128,
                FilePath = "Resources/" + request.File.FileName,
                Size = request.File.Length,
                Format = ".exe"
            };
            content.ContentMetaData = contentMetaData;
            await _contentRepository.Create(content);
            await _contentMetadataRepository.Create(contentMetaData);
            /*if (request.Type == ContentType.Music)
            {
                var musicEntity = new MusicEntity()
                {
                    Content = content,
                    File = fileEntity,
                    Length = 100,
                    ArtistName = "as",
                    MusicText = "asdfasdfasdf"
                };
            }*/
            using (var stream = new FileStream("./Resources/" +request.File.FileName, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }
            
            await _contentMetadataRepository.SaveChangesAsync();
            return new Response(200,new {Message = "the content is added"});
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "the file can not created at time" });
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