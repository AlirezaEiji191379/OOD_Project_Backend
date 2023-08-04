using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories;

public class ChannelPremiumUsersRepository : BaseRepository<ChannelPremiumUsersEntity>,IChannelPremiumUsersRepository
{
    private readonly AppDbContext _appDbContext;
    public ChannelPremiumUsersRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public ValueTask<ChannelMemberEntity?> Find(int userId, int channelId)
    {
        return _appDbContext.ChannelMembers.FindAsync(userId,channelId);
    }
}