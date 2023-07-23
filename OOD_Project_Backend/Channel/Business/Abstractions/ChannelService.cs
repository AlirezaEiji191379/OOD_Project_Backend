using OOD_Project_Backend.Core.Common.Response;

namespace OOD_Project_Backend.Channel.Business.Abstractions;

public interface ChannelService
{
    Task<Response> CreateChannel(string name,int userId);
}