using OOD_Project_Backend.Content.ContentCore.Business.Contexts;
using OOD_Project_Backend.Content.ContentCore.Business.Contracts;
using OOD_Project_Backend.Content.ContentCore.Business.Models.Contract;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.Business.Models;

public class ImageModel : IContentModel
{
    private readonly IImageRepository _imageRepository;
    private readonly IFileEntityRepository _fileEntityRepository;
    private readonly IContentRepository _contentRepository;
    public ContentType ContentType => ContentType.Image;
    
    public ImageModel(IContentRepository contentRepository,
        IFileEntityRepository fileEntityRepository,
        IImageRepository imageRepository)
    {
        _contentRepository = contentRepository;
        _fileEntityRepository = fileEntityRepository;
        _imageRepository = imageRepository;
    }

    public async Task<ShowContentDto> ShowPreview(int contentId)
    {
        return new ShowContentDto()
        {
            ContentType = ContentType.Image.ToString(),
            Value = "the image does not have preview"
        };
    }

    public async Task<ShowContentDto> ShowNormal(int contentId)
    {
        var imageEntity = await _imageRepository.FindById(contentId);
        var filePath = imageEntity.File.FilePath;
        //var contentType = "video/mp4";
        //return new FileContentResult(await File.ReadAllBytesAsync(filePath),contentType);
        return new ShowContentDto()
        {
            Value = Path.GetFileName(filePath),
            ContentType = ContentType.Video.ToString()
        };
    }
    
    public async Task Delete(int contentId)
    {
        var imageEntity = await _imageRepository.FindById(contentId);
        var fileEntity = imageEntity.File;
        File.Delete(imageEntity.File.FilePath);
        _contentRepository.Delete(new ContentEntity()
        {
            Id = contentId
        });
        _fileEntityRepository.Delete(fileEntity);
        await _fileEntityRepository.SaveChangesAsync();
    }
}