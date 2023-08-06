using OOD_Project_Backend.Channel.Business.Context;
using OOD_Project_Backend.Channel.Business.Contracts;
using OOD_Project_Backend.Channel.ChannelCore.Business.Contracts;

namespace OOD_Project_Backend.Channel
{
    public class ChannelFacade : IChannelFacade
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IChannelMembershipService _channelMembershipService;

        public ChannelFacade(ISubscriptionService subscriptionService, IChannelMembershipService channelMembershipService)
        {
            _subscriptionService = subscriptionService;
            _channelMembershipService = channelMembershipService;
        }
        
        public async  Task<bool> CheckAccessToContent(int userId, int channelId, int contentId) {
            return await  IsChannelAdminOrOwner(userId,channelId)  || 
            await _subscriptionService.CheckContentToShowUser(userId, channelId, contentId);
        }

        public async Task<bool> IsChannelAdminOrOwner(int userId, int channelId) {
            return await _channelMembershipService.IsAdmin(userId, channelId) || await _channelMembershipService.IsOwner(userId, channelId);
        }
        
    }
}
