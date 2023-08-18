using OOD_Project_Backend.Content.ContentCore.Business.Contexts;
using OOD_Project_Backend.Content.ContentCore.Business.Creation.Strategies.Contracts;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Content.ContentCore.Business.Creation.Strategies;

public class MusicCreationStrategy : IContentCreationStrategy
{
    public ContentType ContentType => ContentType.Music;
    private readonly IMusicRepository _musicRepository;
    private readonly IFileEntityRepository _fileEntityRepository;
    private readonly IConfiguration _configuration;

    public MusicCreationStrategy(IMusicRepository musicRepository, IFileEntityRepository fileEntityRepository,
        IConfiguration configuration)
    {
        _musicRepository = musicRepository;
        _fileEntityRepository = fileEntityRepository;
        _configuration = configuration;
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
        var musicEntity = new MusicEntity()
        {
            Content = content,
            File = fileEntity,
            ArtistName = "",
            MusicText = ""
        };
        await _fileEntityRepository.Create(fileEntity);
        await _musicRepository.Create(musicEntity);
        await using var stream = new FileStream(fileEntity.FilePath, FileMode.Create);
        await request.File!.CopyToAsync(stream);
    }

    public async Task UpdateContent(ContentUpdateRequest updateRequest)
    {
        var filePath = _configuration.GetValue<string>("Contents") +
                       $"{updateRequest.ContentId}{Path.GetExtension(updateRequest.File!.FileName)}";
        var musicEntity = await _musicRepository.FindById(updateRequest.ContentId);
        var fileEntity = await _fileEntityRepository.FindById(musicEntity.FileId);
        var oldFilePath = fileEntity.FilePath;
        fileEntity.FilePath = filePath;
        fileEntity.Size = updateRequest.File.Length;
        _fileEntityRepository.Update(fileEntity);
        if (File.Exists(oldFilePath))
        {
            File.Delete(oldFilePath);    
        }
        var oldPreview = GetPreviewFilePath(oldFilePath);
        if (File.Exists(oldPreview))
        {
            File.Delete(oldPreview);
        }
        await using var stream = new FileStream(fileEntity.FilePath, FileMode.Create);
        await updateRequest.File!.CopyToAsync(stream);
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