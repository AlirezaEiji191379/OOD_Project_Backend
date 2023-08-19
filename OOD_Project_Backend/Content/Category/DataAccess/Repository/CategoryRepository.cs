using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.Category.DataAccess.Entities;
using OOD_Project_Backend.Content.Category.DataAccess.Repository.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.Category.DataAccess.Repository;

public class CategoryRepository : BaseRepository<CategoryEntity>,ICategoryRepository
{
    private readonly AppDbContext _appDbContext;
    public CategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public async Task<CategoryEntity?> GetByName(int channelId, string name)
    {
        try
        {
            var category = await _appDbContext.CategoryEntities.AsNoTracking()
                .SingleAsync(x => x.ChannelId == channelId && x.Title == name);
            return category;
        }
        catch
        {
            return null;
        }
    }

    public async Task<CategoryEntity?> GetById(int categoryId)
    {
        try
        {
            var category = await _appDbContext.CategoryEntities.AsNoTracking()
                .SingleAsync(x => x.Id == categoryId);
            return category;
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<CategoryEntity>> FindByChannelId(int channelId)
    {
        try
        {
            var categories = await _appDbContext.CategoryEntities.AsNoTracking()
                .Where(x => x.ChannelId == channelId).ToListAsync();
            return categories;
        }
        catch
        {
            return new List<CategoryEntity>();
        }
    }
}