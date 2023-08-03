using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Entities.Enums;
using OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories;

public class ChannelMemberRepository : BaseRepository<ChannelMemberEntity>, IChannelMemberRepository
{
    private readonly AppDbContext _appDbContext;

    public ChannelMemberRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }

    public Task<bool> CheckIfUserIsChannelOwner(int userId, int channelId)
    {
        return _appDbContext.ChannelMembers.AsNoTracking()
            .Where(channelMember => channelMember.ChannelId == channelId && 
                                    channelMember.UserId == userId &&
                                    channelMember.Role == Role.OWNER)
            .AnyAsync();
    }
}