using System.Net;
using System.Text;
using OOD_Project_Backend.Content.Business.Contexts;
using OOD_Project_Backend.Content.Business.Creation.Strategies;
using OOD_Project_Backend.Content.Business.Creation.Strategies.Contracts;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Content.Business.Creation;

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
    
}