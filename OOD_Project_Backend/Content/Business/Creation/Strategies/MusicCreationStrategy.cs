using OOD_Project_Backend.Content.Business.Contexts;
using OOD_Project_Backend.Content.Business.Creation.Strategies.Contracts;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Content.Business.Creation.Strategies;

public class MusicCreationStrategy : IContentCreationStrategy
{
    public ContentType ContentType => ContentType.Music;
    private readonly IMusicRepository _musicRepository;
    private readonly IFileEntityRepository _fileEntityRepository;
    private readonly IConfiguration _configuration;

    public MusicCreationStrategy(IMusicRepository musicRepository, IFileEntityRepository fileEntityRepository, IConfiguration configuration)
    {
        _musicRepository = musicRepository;
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
        var musicEntity = new MusicEntity()
        {
            Content = content,
            File = fileEntity,
            ArtistName = "",
            MusicText = ""
        };
        await _fileEntityRepository.Create(fileEntity);
        await _musicRepository.Create(musicEntity);
        using (var stream = new FileStream(fileEntity.FilePath, FileMode.Create))
        {
            await request.File!.CopyToAsync(stream);
        }
    }
}