using OOD_Project_Backend.Content.ContentCore.Business.Contexts;
using OOD_Project_Backend.Content.ContentCore.Business.Creation.Contracts;
using OOD_Project_Backend.Content.ContentCore.Business.Creation.Strategies.Contracts;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.ContentCore.Business.Creation;

public class ContentCreation : IContentCreation
{
    private readonly IContentMetaDataRepository _contentMetaDataRepository;
    private readonly IContentRepository _contentRepository;
    private readonly IContentCreationStrategyProvider _contentCreationStrategyProvider;

    public ContentCreation(IContentMetaDataRepository contentMetaDataRepository,
        IContentRepository contentRepository,
        IContentCreationStrategyProvider contentCreationStrategyProvider)
    {
        _contentMetaDataRepository = contentMetaDataRepository;
        _contentRepository = contentRepository;
        _contentCreationStrategyProvider = contentCreationStrategyProvider;
    }


    public async Task<int> Generate(ContentCreationRequest request)
    {
        await using var transaction = await _contentRepository.BeginTransactionAsync();
        try
        {
            var contentCreationStrategy = _contentCreationStrategyProvider.GetContentCreationStrategy(request.Type);
            var content = new ContentEntity()
            {
                Description = request.Description,
                Title = request.Title,
                CreatedAt = DateTime.Now.ToUniversalTime()
            };
            var contentMetadata = new ContentMetaDataEntity()
            {
                Content = content,
                ChannelId = request.ChannelId,
                Premium = request.IsPremium,
                Price = request.IsPremium ? request.Price : 0,
                ContentType = request.Type,
                FileName = request.Type != ContentType.Text ? request.File!.FileName : string.Empty
            };
            await _contentRepository.Create(content);
            await _contentMetaDataRepository.Create(contentMetadata);
            await _contentRepository.SaveChangesAsync();
            await contentCreationStrategy.Generate(request,content);
            await _contentRepository.SaveChangesAsync();
            await transaction.CommitAsync();
            return content.Id;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateContent(ContentUpdateRequest updateRequest)
    {
        await using var transaction = await _contentRepository.BeginTransactionAsync();
        try
        {
            var contentMetaData = await _contentMetaDataRepository.FindByContentId(updateRequest.ContentId);
            var content = await _contentRepository.FindById(updateRequest.ContentId);
            if (updateRequest.Description != null || updateRequest.Title != null)
            {
                if (updateRequest.Description != null)
                {
                    content.Description = updateRequest.Description;
                }
                if (updateRequest.Title != null)
                {
                    content.Title = updateRequest.Title;
                }
            }
            if (updateRequest.Price != null || updateRequest.CategoryId != null)
            {
                if (updateRequest.Price != null)
                {
                    contentMetaData.Price = updateRequest.Price.Value;
                }
                if (updateRequest.CategoryId != null)
                {
                    contentMetaData.CategoryId = updateRequest.CategoryId;
                }
            }

            if (updateRequest.File != null || updateRequest.Value != null)
            {
                if (updateRequest.File != null)
                {
                    contentMetaData.FileName = updateRequest.File.FileName;
                }
                var contentType = contentMetaData.ContentType;
                var contentCreationStrategy = _contentCreationStrategyProvider.GetContentCreationStrategy(contentType);
                await contentCreationStrategy.UpdateContent(updateRequest);
            }
            _contentRepository.Update(content);
            _contentMetaDataRepository.Update(contentMetaData);
            
            await _contentMetaDataRepository.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}