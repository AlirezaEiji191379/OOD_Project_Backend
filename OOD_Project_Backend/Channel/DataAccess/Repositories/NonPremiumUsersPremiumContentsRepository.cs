using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories;

public class NonPremiumUsersPremiumContentsRepository : BaseRepository<NonPremiumUsersPremiumContentsEntity>,INonPremiumUsersPremiumContentsRepository
{
    private readonly AppDbContext _appDbContext;
    public NonPremiumUsersPremiumContentsRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public ValueTask<NonPremiumUsersPremiumContentsEntity?> Find(int contentId, int userId)
    {
        return _appDbContext.NonPremiumUsersPremiumContents.FindAsync(userId, contentId);
    }
}