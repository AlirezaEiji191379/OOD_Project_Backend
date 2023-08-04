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

    public MusicCreationStrategy(IMusicRepository musicRepository)
    {
        _musicRepository = musicRepository;
    }

    public async Task Generate(ContentCreationRequest contentCreationRequest,FileEntity fileEntity,ContentEntity content)
    {
        var musicEntity = new MusicEntity()
        {
            Content = content,
            File = fileEntity,
            ArtistName = "",
            MusicText = ""
        };
        await _musicRepository.Create(musicEntity);
    }
}