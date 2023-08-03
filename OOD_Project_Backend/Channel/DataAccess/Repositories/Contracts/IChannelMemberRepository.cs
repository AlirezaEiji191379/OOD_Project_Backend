using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Entities.Enums;
using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.User.Business.Context;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;

public interface IChannelMemberRepository : IBaseRepository<ChannelMemberEntity>
{
    Task<bool> CheckIfUserIsChannelOwner(int userId, int channelId);
    Task<bool> IsChannelMember(int userId, int channelId);
    Task<List<ChannelMemberEntity>> FindByMemberId(int userId);
    Task<List<UserContract>> FindUsersByChannelId(int channelId);
    Task<ChannelMemberEntity> FindByUserIdAndChannelId(int userId, int channelId);
    void UpdateRoleOfUserInChannel(int userId, int channelId, Role role);
    Task<bool> IsOwner(int userId, int channelId);
    Task<bool> IsAdmin(int userId,int channelId);
    Task<List<ChannelMemberEntity>> FindByChannelIdAndRole(int channelId, params Role[] roles);
    Task<List<ChannelMemberEntity>> FindByChannelId(int channelId);
}