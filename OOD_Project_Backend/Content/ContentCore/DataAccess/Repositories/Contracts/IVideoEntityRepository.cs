using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

public interface IVideoEntityRepository : IBaseRepository<VideoEntity>
{
    Task<VideoEntity> FindById(int contentId);
}