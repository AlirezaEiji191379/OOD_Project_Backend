using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Channel.Business.Contracts;

public interface IChannelService
{
    Task<Response> CreateChannel(string name,int userId);
    Task<Response> ShowChannelsList(int userId);
    Task<Response> AddChannelPicture(IFormFile formFile, int userId, int channelId);
}