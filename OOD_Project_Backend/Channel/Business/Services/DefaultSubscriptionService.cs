using OOD_Project_Backend.Channel.Business.Context;
using OOD_Project_Backend.Channel.Business.Contracts;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Channel.Business.Services;

public class DefaultSubscriptionService : ISubscriptionService
{
    public Task<bool> CheckContentToShowUser(int userId, int channelId, int contentId)
    {
        throw new NotImplementedException();
    }

    public Task<Response> AddSubscription(SubscriptionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response> EditSubscription(SubscriptionRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response> ShowSubscription(int channelId)
    {
        throw new NotImplementedException();
    }

    public Task<Response> BuySubscription(int subscriptionId)
    {
        throw new NotImplementedException();
    }

    public Task<Response> BuyContent(int contentId)
    {
        throw new NotImplementedException();
    }
}