using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;
using OOD_Project_Backend.User.Business.Context;
using OOD_Project_Backend.User.DataAccess.Entities;
using OOD_Project_Backend.User.DataAccess.Repositories.Contract;

namespace OOD_Project_Backend.User.DataAccess.Repositories;

internal class UserRepository : BaseRepository<UserEntity>,IUserRepository
{
    private readonly AppDbContext _appDbContext;
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public async Task<UserEntity> FindByUserId(int userId,bool trackChange)
    {
        return (trackChange
            ? await _appDbContext.Users.Where(user => user.Id == userId).FirstOrDefaultAsync()
            : await _appDbContext.Users.Where(user => user.Id == userId).AsNoTracking().FirstOrDefaultAsync())!;
    }

    public async Task<UserContract> GetUserProfile(int userId)
    {
        return await _appDbContext.Users.Where(user => user.Id == userId).AsNoTracking().Select(x => new UserContract()
        {
            Biography = x.Biography,
            Name = x.Name,
            UserId = userId
        }).SingleAsync();
    }
}