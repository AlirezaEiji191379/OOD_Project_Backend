namespace OOD_Project_Backend.Channel.Business.Contracts
{
    public interface IChannelFacade
    {
        Task<bool> CheckAccessToContent(int userId, int channelId, int contentId);
        Task<bool> IsChannelAdminOrOwner(int userId, int channelId);
    }
}
