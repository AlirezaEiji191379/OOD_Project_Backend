using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Channel.Business.Abstractions;

public interface ChannelService
{
    Task<Response> CreateChannel(string name,int userId);
    Task<Response> GetAllUsersChannels(int userId);
}