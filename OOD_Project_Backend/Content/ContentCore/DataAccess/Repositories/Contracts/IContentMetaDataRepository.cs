using OOD_Project_Backend.Content.ContentCore.Business.Contracts;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

public interface IContentMetaDataRepository : IBaseRepository<ContentMetaDataEntity>
{
    Task<ContentMetaDataEntity> FindByContentId(int contentId);
    // include content
    Task<List<ContentDto>> FindByChannelIdIncludeContent(int channelId);
    Task<List<ContentDto>> FindByChannelIdIncludeContentAndLikeTitle(int channelId, string title);
    Task<List<ContentDto>> FindByLikeTitle(string title);
    Task<ContentMetaDataEntity> FindByChannelId(int contentId);
    Task<List<ContentDto>> FindRandom();
    Task<List<ContentMetaDataEntity>> FindByCategoryId(int categoryId);

}