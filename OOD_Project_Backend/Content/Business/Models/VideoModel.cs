using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Content.Business.Models.Contract;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Content.Business.Models;

public class VideoModel : IContentModel
{
    private readonly IVideoEntityRepository _videoEntityRepository;

    public VideoModel(IVideoEntityRepository videoEntityRepository)
    {
        _videoEntityRepository = videoEntityRepository;
    }

    public ContentType ContentType => ContentType.Video;
    public Task<FileResult> ShowPreview(int contentId)
    {
        throw new NotImplementedException();
    }

    public async Task<FileResult> ShowNormal(int contentId)
    {
        var videoEntity = await _videoEntityRepository.FindById(contentId);
        var filePath = videoEntity.File.FilePath;
        var contentType = "video/mp4";
        return new FileContentResult(await File.ReadAllBytesAsync(filePath),contentType);
    }
}