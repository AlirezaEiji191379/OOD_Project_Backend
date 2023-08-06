using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Channel.Subscription.Business.Context;
using OOD_Project_Backend.Channel.Subscription.Business.Contracts;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Channel.Subscription.Controller;

[ApiController]
[Route("Subscription")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost]
    [Route("AddSubscription")]
    [Authorize]
    public async Task<Response> AddSubscription([FromBody] SubscriptionRequest request)
    {
        return await _subscriptionService.AddSubscription(request);
    }

    [HttpGet]
    [Route("{channelId}")]
    [Authorize]
    public async Task<Response> ShowSubscription(int channelId)
    {
        return await _subscriptionService.ShowSubscription(channelId);
    }

    [HttpPost]
    [Route("BuySubscription/{subscriptionId}")]
    [Authorize]
    public async Task<Response> BuySubscription(int subscriptionId)
    {
        return await _subscriptionService.BuySubscription(subscriptionId);
    }

    [HttpPost]
    [Route("BuyContent/{contentId}")]
    [Authorize]
    public async Task<Response> BuyContent(int contentId)
    {
        return await _subscriptionService.BuyContent(contentId);
    }
    
}