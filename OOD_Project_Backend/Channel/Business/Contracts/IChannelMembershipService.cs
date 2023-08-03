using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Channel.Business.Contracts;

public interface IChannelMembershipService
{
    Task<Response> JoinChannel(string joinLink, int userId);
    Task<Response> ShowMembers(int channelId);
}