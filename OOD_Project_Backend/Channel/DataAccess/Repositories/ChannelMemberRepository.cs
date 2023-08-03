﻿using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Channel.Business.Context;
using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Entities.Enums;
using OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;
using OOD_Project_Backend.User.Business.Context;

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

    public Task<bool> IsChannelMember(int userId, int channelId)
    {
        return _appDbContext.ChannelMembers.AsNoTracking()
            .Where(cm => cm.UserId == userId && cm.ChannelId == channelId)
            .AnyAsync();
    }

    public Task<List<ChannelMemberEntity>> FindByMemberId(int userId)
    {
        return _appDbContext.ChannelMembers.AsNoTracking().Where(x => x.UserId == userId)
            .Include(x => x.Channel)
            .ToListAsync();
    }

    public Task<List<UserContract>> FindUsersByChannelId(int channelId)
    {
        return _appDbContext.ChannelMembers.AsNoTracking()
            .Where(cm => cm.ChannelId == channelId)
            .Include(x => x.User)
            .Select(x => new UserContract()
            {
                UserId = x.UserId,
                Name = x.User.Name
            })
            .ToListAsync();
    }

    public Task<ChannelMemberEntity> FindByUserIdAndChannelId(int userId, int channelId)
    {
        return _appDbContext.ChannelMembers.AsNoTracking()
            .Where(cm => cm.ChannelId == channelId && cm.UserId == userId)
            .SingleAsync();
    }

    public void UpdateRoleOfUserInChannel(int userId, int channelId, Role role)
    {
        var channelMemberEntity = new ChannelMemberEntity()
        {
            UserId = userId,
            ChannelId = channelId,
            Role = role
        };
        _appDbContext.Attach(channelMemberEntity);
        _appDbContext.Entry(channelMemberEntity).Property(x => x.Role).IsModified = true;
    }
}