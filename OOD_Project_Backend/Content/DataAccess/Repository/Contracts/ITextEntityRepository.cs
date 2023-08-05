using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Content.DataAccess.Repository.Contracts;

public interface ITextEntityRepository : IBaseRepository<TextEntity>
{
    Task<TextEntity> FindByContentId(int contentId);
}