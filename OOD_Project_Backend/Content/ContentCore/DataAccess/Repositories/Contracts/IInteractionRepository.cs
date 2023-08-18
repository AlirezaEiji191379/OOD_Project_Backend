using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;

public interface IInteractionRepository : IBaseRepository<InteractionEntity>
{
    Task<bool> IsUserLikedContent(int userId,int contentId);
}