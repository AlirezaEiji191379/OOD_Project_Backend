using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Entities;
using OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Content.ContentCore.DataAccess.Repositories;

public class InteractionRepository : BaseRepository<InteractionEntity>,IInteractionRepository
{
    private readonly AppDbContext _dbContext;

    public InteractionRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsUserLikedContent(int userId, int contentId)
    {
        return await _dbContext.Likes.AnyAsync(x => x.UserId == userId && x.ContentId == contentId);
    }
}