using OOD_Project_Backend.Content.ContentCore.Business.Contracts;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

public interface IContentMetaDataRepository : IBaseRepository<ContentMetaDataEntity>
{
    Task<ContentMetaDataEntity> FindByContentId(int contentId);
    // include content
    Task<List<ContentDto>> FindByChannelIdIncludeContent(int channelId);
    Task<ContentMetaDataEntity> FindByChannelId(int contentId);
}