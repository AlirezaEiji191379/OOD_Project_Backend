using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories;

public class ContentRepository : BaseRepository<ContentEntity>,IContentRepository
{
    private readonly AppDbContext _appDbContext;
    public ContentRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }
}