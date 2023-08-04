using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

public interface IContentMetaDataRepository : IBaseRepository<ContentMetaDataEntity>
{
    Task<ContentMetaDataEntity> FindByContentId(int contentId);
}