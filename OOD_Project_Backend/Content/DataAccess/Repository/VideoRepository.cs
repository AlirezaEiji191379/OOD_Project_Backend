using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.DataAccess.Repository;

public class VideoRepository : BaseRepository<VideoEntity>,IVideoEntityRepository
{
    private readonly AppDbContext _appDbContext;
    public VideoRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public Task<VideoEntity> FindById(int contentId)
    {
        return _appDbContext.Videos.Where(x => x.ContentId == contentId)
            .Include(x => x.File)
            .SingleAsync();
    }
}