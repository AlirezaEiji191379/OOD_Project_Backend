using OOD_Project_Backend.Channel.Business.Context;
using OOD_Project_Backend.Channel.Business.Contracts;
using OOD_Project_Backend.Channel.Business.Validations.Conditions;
using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Core.Validation.Contracts;
using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.Channel.Business.Services;

public class DefaultSubscriptionService : ISubscriptionService
{
    private readonly IUserFacade _userFacade;
    private readonly IChannelMembershipService _channelMembershipService;
    private readonly IValidator _validator;
    private readonly ISubscriptionRepository _subscriptionRepository;

    public DefaultSubscriptionService(IUserFacade userFacade, 
        IChannelMembershipService channelMembershipService, 
        IValidator validator, 
        ISubscriptionRepository subscriptionRepository)
    {
        _userFacade = userFacade;
        _channelMembershipService = channelMembershipService;
        _validator = validator;
        _subscriptionRepository = subscriptionRepository;
    }


    public Task<bool> CheckContentToShowUser(int userId, int channelId, int contentId)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> AddSubscription(SubscriptionRequest request)
    {
        try
        {
            if (!_validator.Validate(new PriceRule(request.Price)))
            {
                return new Response(400, new { Message = "invalid subscription request!" });
            }

            var userId = _userFacade.GetCurrentUserId();
            if (await _channelMembershipService.IsOwner(request.ChannelId, userId) == false)
            {
                return new Response(403, new { Message = "only owners can add subscriptions!" });
            }

            var subscriptionEntity = new SubscriptionEntity()
            {
                ChannelId = request.ChannelId,
                Period = request.Period,
                Price = request.Price
            };
            await _subscriptionRepository.Create(subscriptionEntity);
            await _subscriptionRepository.SaveChangesAsync();
            return new Response(201, new { Message = "subscription added to the channel!" });
        }
        catch (Exception e)
        {
            return new Response(400, new { Message = "failed to add subscription!" });
        }
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