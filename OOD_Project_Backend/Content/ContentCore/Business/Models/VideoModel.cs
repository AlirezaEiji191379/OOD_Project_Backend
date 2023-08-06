using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.ContentCore.Business.Contracts;
using OOD_Project_Backend.Content.ContentCore.Business.Models.Contract;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.Business.Models;

public class VideoModel : IContentModel
{
    private readonly IVideoEntityRepository _videoEntityRepository;
    private readonly IFileEntityRepository _fileEntityRepository;
    private readonly IContentRepository _contentRepository;
    public VideoModel(IVideoEntityRepository videoEntityRepository, IFileEntityRepository fileEntityRepository, IContentRepository contentRepository)
    {
        _videoEntityRepository = videoEntityRepository;
        _fileEntityRepository = fileEntityRepository;
        _contentRepository = contentRepository;
    }

    public ContentType ContentType => ContentType.Video;
    public async Task<ShowContentDto> ShowPreview(int contentId)
    {
        var videoEntity = await _videoEntityRepository.FindById(contentId);
        var filePath = videoEntity.File.FilePath;
        var appendString = "_preview";
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var newFileName = fileName + appendString;
        /*var fileDirectory = Path.GetDirectoryName(filePath);*/
        var fileExtension = Path.GetExtension(filePath);
        //var previewFilePath = Path.Combine(fileDirectory, newFileName + fileExtension);
        return new ShowContentDto()
        {
            ContentType = ContentType.Video.ToString(),
            Value = newFileName + fileExtension
        };
    }

    public async Task<ShowContentDto> ShowNormal(int contentId)
    {
        var videoEntity = await _videoEntityRepository.FindById(contentId);
        var filePath = videoEntity.File.FilePath;
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
        var videoEntity = await _videoEntityRepository.FindById(contentId);
        var fileEntity = videoEntity.File;
        File.Delete(videoEntity.File.FilePath);
        _contentRepository.Delete(new ContentEntity()
        {
            Id = contentId
        });
        _fileEntityRepository.Delete(fileEntity);
        await _fileEntityRepository.SaveChangesAsync();
    }
}