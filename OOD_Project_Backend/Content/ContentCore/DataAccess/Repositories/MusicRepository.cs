using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories;

public class MusicRepository : BaseRepository<MusicEntity>,IMusicRepository
{
    private readonly AppDbContext _appDbContext;
    public MusicRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public Task<MusicEntity> FindById(int contentId)
    {
        return _appDbContext.Musics.Where(x => x.ContentId == contentId)
            .Include(x => x.File)
            .SingleAsync();
    }
}