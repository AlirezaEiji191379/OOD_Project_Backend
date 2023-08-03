using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories;

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
}