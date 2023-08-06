using OOD_Project_Backend.Content.Category.DataAccess.Entities;
using OOD_Project_Backend.Content.Category.DataAccess.Repository.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.Category.DataAccess.Repository;

public class CategoryRepository : BaseRepository<CategoryEntity>,ICategoryRepository
{
    public CategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}