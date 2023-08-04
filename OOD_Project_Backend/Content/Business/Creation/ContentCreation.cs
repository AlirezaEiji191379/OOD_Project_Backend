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
    private readonly IConfiguration _configuration;

    public ContentCreation(IContentMetaDataRepository contentMetaDataRepository,
        IContentRepository contentRepository,
        IContentCreationStrategyProvider contentCreationStrategyProvider,
        IConfiguration configuration)
    {
        _contentMetaDataRepository = contentMetaDataRepository;
        _contentRepository = contentRepository;
        _contentCreationStrategyProvider = contentCreationStrategyProvider;
        _configuration = configuration;
    }


    public async Task Generate(ContentCreationRequest request)
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
            Price = request.Price,
            ContentType = request.Type,
            FileName = request.Type != ContentType.Text ? request.File!.FileName : string.Empty
        };
        var fileEntity = new FileEntity()
        {
            Format = Path.GetExtension(request.File.FileName),
            Quality = FileQuality._480,
            Size = request.File.Length,
            FilePath = _configuration.GetValue<string>("Contents")
        };
        await _contentRepository.Create(content);
        await _contentMetaDataRepository.Create(contentMetadata);
        await contentCreationStrategy.Generate(request,fileEntity,content);
        await _contentRepository.SaveChangesAsync();
    }
    
}