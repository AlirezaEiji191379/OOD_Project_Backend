using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

public interface IMusicRepository : IBaseRepository<MusicEntity>
{
    Task<MusicEntity> FindById(int contentId);
}