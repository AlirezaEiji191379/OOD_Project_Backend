using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories;

public class ImageRepository : BaseRepository<ImageEntity>, IImageRepository
{
    private readonly AppDbContext _dbContext;

    public ImageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ImageEntity> FindById(int contentId)
    {
        return _dbContext.Images.Where(x => x.ContentId == contentId)
            .Include(x => x.File)
            .SingleAsync();
    }
}