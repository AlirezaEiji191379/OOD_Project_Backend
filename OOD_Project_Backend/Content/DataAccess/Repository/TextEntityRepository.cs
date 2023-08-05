using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.DataAccess.Entities;
using OOD_Project_Backend.Content.DataAccess.Repository.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.DataAccess.Repository;

public class TextEntityRepository : BaseRepository<TextEntity>,ITextEntityRepository
{
    private readonly AppDbContext _appDbContext;
    public TextEntityRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public Task<TextEntity> FindByContentId(int contentId)
    {
        return _appDbContext.Texts.Where(x => x.ContentId == contentId).SingleAsync();
    }
}