using OOD_Project_Backend.Content.Category.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Content.Category.DataAccess.Repository.Contracts;

public interface ICategoryRepository : IBaseRepository<CategoryEntity>
{
    Task<CategoryEntity?> GetByName(int channelId, string name);
    Task<CategoryEntity?> GetById(int categoryId);
    Task<List<CategoryEntity>> FindByChannelId(int channelId);
}