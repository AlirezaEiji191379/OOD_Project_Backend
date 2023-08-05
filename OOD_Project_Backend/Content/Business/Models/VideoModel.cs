using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NAudio.Wave;
using OOD_Project_Backend.Content.Business.Models.Contract;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Content.Business.Models;

public class VideoModel : IContentModel
{
    private readonly IVideoEntityRepository _videoEntityRepository;
    private readonly IFileEntityRepository _fileEntityRepository;
    public VideoModel(IVideoEntityRepository videoEntityRepository, IFileEntityRepository fileEntityRepository)
    {
        _videoEntityRepository = videoEntityRepository;
        _fileEntityRepository = fileEntityRepository;
    }

    public ContentType ContentType => ContentType.Video;
    public async Task<FileResult> ShowPreview(int contentId)
    {
        var videoEntity = await _videoEntityRepository.FindById(contentId);
        var filePath = videoEntity.File.FilePath;
        var contentType = "video/mp4";
        string ffmpegCommand = $"-ss 00:00:30 -t 00:00:10 -i {filePath} -c:v copy -c:a copy -f mp4 -";
        var processInfo = new ProcessStartInfo
        {
            FileName = "ffmpeg",
            Arguments = ffmpegCommand,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (var process = new Process { StartInfo = processInfo })
        {
            process.Start();
            using (var memoryStream = new MemoryStream())
            {
                process.StandardOutput.BaseStream.CopyTo(memoryStream);
                await process.WaitForExitAsync();
                return new FileContentResult(memoryStream.ToArray(), contentType);
            }
        }
    }

    public async Task<FileResult> ShowNormal(int contentId)
    {
        var videoEntity = await _videoEntityRepository.FindById(contentId);
        var filePath = videoEntity.File.FilePath;
        var contentType = "video/mp4";
        return new FileContentResult(await File.ReadAllBytesAsync(filePath),contentType);
    }

    public async Task Delete(int contentId)
    {
        var videoEntity = await _videoEntityRepository.FindById(contentId);
        var fileEntity = videoEntity.File;
        File.Delete(videoEntity.File.FilePath);
        _fileEntityRepository.Delete(fileEntity);
        await _fileEntityRepository.SaveChangesAsync();
    }
}