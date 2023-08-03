using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;

public interface IChannelMemberRepository : IBaseRepository<ChannelMemberEntity>
{
    Task<bool> CheckIfUserIsChannelOwner(int userId, int channelId);
    Task<bool> IsChannelMember(int userId, int channelId);
}