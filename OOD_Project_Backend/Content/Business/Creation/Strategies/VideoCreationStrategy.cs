using OOD_Project_Backend.Content.Business.Contexts;
using OOD_Project_Backend.Content.Business.Creation.Strategies.Contracts;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Entities.Enums;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Content.Business.Creation.Strategies;

public class VideoCreationStrategy : IContentCreationStrategy
{
    public ContentType ContentType => ContentType.Video;
    private readonly IVideoEntityRepository _videoEntityRepository;
    
    public VideoCreationStrategy(IVideoEntityRepository videoEntityRepository)
    {
        _videoEntityRepository = videoEntityRepository;
    }

    public async Task Generate(ContentCreationRequest request,FileEntity fileEntity,ContentEntity content)
    {
        var videoEntity = new VideoEntity()
        {
            Content = content,
            File = fileEntity,
        };
        await _videoEntityRepository.Create(videoEntity);
    }
}