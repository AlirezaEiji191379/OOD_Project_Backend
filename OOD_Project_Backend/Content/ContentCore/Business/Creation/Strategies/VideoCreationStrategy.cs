using System.Diagnostics;
using OOD_Project_Backend.Content.ContentCore.Business.Contexts;
using OOD_Project_Backend.Content.ContentCore.Business.Creation.Strategies.Contracts;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.Business.Creation.Strategies;

public class VideoCreationStrategy : IContentCreationStrategy
{
    public ContentType ContentType => ContentType.Video;
    private readonly IVideoEntityRepository _videoEntityRepository;
    private readonly IFileEntityRepository _fileEntityRepository;
    private readonly IConfiguration _configuration;
    public VideoCreationStrategy(IVideoEntityRepository videoEntityRepository, IFileEntityRepository fileEntityRepository, IConfiguration configuration)
    {
        _videoEntityRepository = videoEntityRepository;
        _fileEntityRepository = fileEntityRepository;
        _configuration = configuration;
    }

    public async Task Generate(ContentCreationRequest request,ContentEntity content)
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
        var videoEntity = new VideoEntity()
        {
            Content = content,
            File = fileEntity,
        };
        await _videoEntityRepository.Create(videoEntity);
        await _fileEntityRepository.Create(fileEntity);
        using (var stream = new FileStream(fileEntity.FilePath, FileMode.Create))
        {
            await request.File!.CopyToAsync(stream);
        }
        await CreatePreviewFile(filePath);
    }

    private async Task CreatePreviewFile(string filePath)
    {
        var previewFilePath = GetPreviewFilePath(filePath);
        var ffmpegProcess = new Process
        {
            StartInfo =
            {
                FileName =  _configuration.GetValue<string>("FFMPEG"),
                Arguments = $"-i \"{filePath}\" -ss 00:00:00 -to 00:00:05 -c copy \"{previewFilePath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        ffmpegProcess.Start();
        await ffmpegProcess.WaitForExitAsync();
    }

    private string GetPreviewFilePath(string filePath)
    {
        var appendString = "_preview";
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var newFileName = fileName + appendString;
        var fileDirectory = Path.GetDirectoryName(filePath);
        var fileExtension = Path.GetExtension(filePath);
        return Path.Combine(fileDirectory, newFileName + fileExtension);
    }
    
}