using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories;

public class ChannelMemberRepository : BaseRepository<ChannelMemberEntity>
{
    public ChannelMemberRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}