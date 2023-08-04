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
    private readonly IFileEntityRepository _fileEntityRepository;
    private readonly IContentCreationStrategyProvider _contentCreationStrategyProvider;
    private readonly IConfiguration _configuration;

    public ContentCreation(IContentMetaDataRepository contentMetaDataRepository,
        IContentRepository contentRepository,
        IContentCreationStrategyProvider contentCreationStrategyProvider,
        IConfiguration configuration,
        IFileEntityRepository fileEntityRepository)
    {
        _contentMetaDataRepository = contentMetaDataRepository;
        _contentRepository = contentRepository;
        _contentCreationStrategyProvider = contentCreationStrategyProvider;
        _configuration = configuration;
        _fileEntityRepository = fileEntityRepository;
    }


    public async Task<int> Generate(ContentCreationRequest request)
    {
        var filePath = request.Type != ContentType.Text
            ? _configuration.GetValue<string>("Contents") + $"{Guid.NewGuid().ToString().Replace("-","")}.{Path.GetExtension(request.File!.FileName)}"
            : string.Empty;
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
            var fileEntity = new FileEntity()
            {
                Format = request.Type != ContentType.Text ? Path.GetExtension(request.File.FileName) : string.Empty,
                Quality = FileQuality._480,
                Size = request.Type != ContentType.Text ? request.File.Length : Encoding.Unicode.GetBytes(request.Value).Length,
                FilePath = filePath
            };
            await _contentRepository.Create(content);
            await _contentMetaDataRepository.Create(contentMetadata);
            await _fileEntityRepository.Create(fileEntity);
            await contentCreationStrategy.Generate(request,fileEntity,content);
            await _contentRepository.SaveChangesAsync();
            await transaction.CommitAsync();
            return content.Id;
        }
        catch (Exception e)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            await transaction.RollbackAsync();
            throw;
        }
    }
    
}