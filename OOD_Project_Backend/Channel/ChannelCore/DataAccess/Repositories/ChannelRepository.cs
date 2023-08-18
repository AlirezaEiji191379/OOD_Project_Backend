using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Channel.ChannelCore.DataAccess.Repositories;

public class ChannelRepository : BaseRepository<ChannelEntity>,IChannelRepository
{
    private readonly AppDbContext _appDbContext;
    public ChannelRepository(AppDbContext dbContext) : base(dbContext)
    {
        _appDbContext = dbContext;
    }
    public async Task<ChannelEntity> FindChannelByJoinLink(string joinLink)
    {
        return await _appDbContext.Channels.AsNoTracking()
            .SingleAsync(x => x.JoinLink == joinLink);
    }

    public Task<ChannelEntity> FindById(int channelId)
    {
        return _appDbContext.Channels.Where(x => x.Id == channelId).SingleAsync();
    }
}