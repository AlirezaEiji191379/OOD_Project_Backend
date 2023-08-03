using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Channel.Business.Context;
using OOD_Project_Backend.Channel.Business.Contracts;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Channel.Controller;

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

}