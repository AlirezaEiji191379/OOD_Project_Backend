using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories;

public class TextRepository : BaseRepository<TextEntity>,ITextRepository
{
    private readonly AppDbContext _appDbContext;
    public TextRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public Task<TextEntity> FindByContentId(int contentId)
    {
        return _appDbContext.Texts.Where(x => x.ContentId == contentId).SingleAsync();
    }
}