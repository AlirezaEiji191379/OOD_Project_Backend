﻿using OOD_Project_Backend.Channel.Business.Context;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Channel.Business.Contracts;

public interface ISubscriptionService
{
    Task<bool> CheckContentToShowUser(int userId, int channelId, int contentId);

    Task<Response> AddSubscription(SubscriptionRequest request);
    Task<Response> EditSubscription(SubscriptionRequest request);
    Task<Response> ShowSubscription(int channelId);
    Task<Response> BuySubscription(int subscriptionId);
    Task<Response> BuyContent(int contentId);
}