using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories;

public class ChannelRepository : BaseRepository<ChannelEntity>
{
    public ChannelRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}