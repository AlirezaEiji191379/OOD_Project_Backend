using OOD_Project_Backend.Channel.ChannelCore.Business.Context;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Channel.ChannelCore.Business.Contracts;

public interface IChannelMembershipService
{
    Task<Response> JoinChannel(string joinLink);
    Task<Response> ShowMembers(int channelId);
    Task<Response> AddAdminToChannel(ChannelMembershipRequest channelMembershipRequest);
    Task<Response> SetIncomeShare(ChannelMembershipRequest channelMembershipRequest);
    Task<bool> IsOwner(int channelId, int userId);
    Task<bool> IsAdmin(int channelId, int userId);
    Task<Response> ShowAdmins(int channelId);
    Task<Response> RemoveMember(ChannelMembershipRequest membershipRequest);
    Task<Response> RemoveAdmin(ChannelMembershipRequest membershipRequest);
    Task<Response> GetChannelsList();
}