using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories;

public class ChannelPremiumUsersRepository : BaseRepository<ChannelPremiumUsersEntity>,IChannelPremiumUsersRepository
{
    public ChannelPremiumUsersRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}