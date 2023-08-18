using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Channel.ChannelCore.DataAccess.Repositories.Contracts;

public interface IChannelRepository : IBaseRepository<ChannelEntity>
{
    Task<ChannelEntity> FindChannelByJoinLink(string joinLink);
    Task<ChannelEntity> FindById(int channelId);
}