using OOD_Project_Backend.Content.ContentCore.Business.Contexts;
using OOD_Project_Backend.Content.ContentCore.Business.Creation.Strategies.Contracts;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.Business.Creation.Strategies;

public class ImageCreationStrategy : IContentCreationStrategy
{
    public ContentType ContentType => ContentType.Image;
    private readonly IFileEntityRepository _fileEntityRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IConfiguration _configuration;
    
    public ImageCreationStrategy(IFileEntityRepository fileEntityRepository, IConfiguration configuration, IImageRepository imageRepository)
    {
        _fileEntityRepository = fileEntityRepository;
        _configuration = configuration;
        _imageRepository = imageRepository;
    }

    public async Task Generate(ContentCreationRequest request, ContentEntity content)
    {
        var filePath = _configuration.GetValue<string>("Contents") +
                       $"{content.Id}{Path.GetExtension(request.File!.FileName)}";
        var fileEntity = new FileEntity()
        {
            Format = request.Type != ContentType.Text ? Path.GetExtension(request.File.FileName) : string.Empty,
            Quality = FileQuality._480,
            Size = request.File.Length,
            FilePath = filePath
        };
        var imageEntity = new ImageEntity()
        {
            Content = content,
            File = fileEntity
        };
        await _fileEntityRepository.Create(fileEntity);
        await _imageRepository.Create(imageEntity);
        await using var stream = new FileStream(fileEntity.FilePath, FileMode.Create);
        await request.File!.CopyToAsync(stream);
    }

    public async Task UpdateContent(ContentUpdateRequest updateRequest)
    {
        var filePath = _configuration.GetValue<string>("Contents") +
                       $"{updateRequest.ContentId}{Path.GetExtension(updateRequest.File!.FileName)}";
        var musicEntity = await _imageRepository.FindById(updateRequest.ContentId);
        var fileEntity = await _fileEntityRepository.FindById(musicEntity.FileId);
        var oldFilePath = fileEntity.FilePath;
        fileEntity.FilePath = filePath;
        fileEntity.Size = updateRequest.File.Length;
        _fileEntityRepository.Update(fileEntity);
        if (File.Exists(oldFilePath))
        {
            File.Delete(oldFilePath);    
        }
        await using var stream = new FileStream(fileEntity.FilePath, FileMode.Create);
        await updateRequest.File!.CopyToAsync(stream);
    }
}