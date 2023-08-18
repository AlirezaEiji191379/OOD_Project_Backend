using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Channel.ChannelCore.DataAccess.Repositories;

public class ChannelPremiumUsersRepository : BaseRepository<ChannelPremiumUsersEntity>,IChannelPremiumUsersRepository
{
    private readonly AppDbContext _appDbContext;
    public ChannelPremiumUsersRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public ValueTask<ChannelPremiumUsersEntity?> Find(int userId, int channelId)
    {
        return _appDbContext.ChannelPremiumUsers.FindAsync(userId,channelId);
    }

    public Task<List<ChannelPremiumUsersEntity>> FindAfterDate(DateTime dateTime)
    {
        return _appDbContext.ChannelPremiumUsers.Where(x => x.EndTime > dateTime && x.Active == true).ToListAsync();
    }
}