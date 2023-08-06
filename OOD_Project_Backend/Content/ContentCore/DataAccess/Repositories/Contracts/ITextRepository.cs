using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

public interface ITextRepository : IBaseRepository<TextEntity>
{
    Task<TextEntity> FindByContentId(int contentId);
}