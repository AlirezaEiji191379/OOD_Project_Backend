using OOD_Project_Backend.Channel.Business.Context;
using OOD_Project_Backend.Channel.Business.Contracts;
using OOD_Project_Backend.Channel.Business.Validations.Conditions;
using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Core.Validation.Contracts;
using OOD_Project_Backend.Finance.Business.Contracts;
using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.Channel.Business.Services;

public class DefaultSubscriptionService : ISubscriptionService
{
    private readonly IUserFacade _userFacade;
    private readonly IChannelMembershipService _channelMembershipService;
    private readonly IValidator _validator;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IChannelMemberRepository _channelMemberRepository;
    private readonly IFinanceFacade _financeFacade;
    
    public DefaultSubscriptionService(IUserFacade userFacade, 
        IChannelMembershipService channelMembershipService, 
        IValidator validator, 
        ISubscriptionRepository subscriptionRepository, 
        IChannelMemberRepository channelMemberRepository,
        IFinanceFacade financeFacade)
    {
        _userFacade = userFacade;
        _channelMembershipService = channelMembershipService;
        _validator = validator;
        _subscriptionRepository = subscriptionRepository;
        _channelMemberRepository = channelMemberRepository;
        _financeFacade = financeFacade;
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

    public async Task<Response> ShowSubscription(int channelId)
    {
        try
        {
            var subEntities = await _subscriptionRepository.FindByChannelId(channelId);
            var subDtos = subEntities.Select(x => new
            {
                Period = x.Period,
                Price = x.Price
            }).ToList();
            return new Response(200,new {Message = subDtos});
        }
        catch (Exception e)
        {
            return new Response(404,new {Message = "channel not found!"});
        }
    }

    public async Task<Response> BuySubscription(int subscriptionId)
    {
        try
        {
            var subscription = await _subscriptionRepository.FindById(subscriptionId);
            if (subscription == null)
            {
                return new Response(400,new {Message = "subscription not found!"});
            }
            var amount = subscription.Price;
            var userId = _userFacade.GetCurrentUserId();
            var members = await _channelMemberRepository.FindByChannelId(subscription.ChannelId);
            //var buyResult = _financeFacade.
            return null;
        }
        catch (Exception e)
        {
            return new Response(400,new {Message = "buy subscription failed!"});
        }
    }

    public Task<Response> BuyContent(int contentId)
    {
        throw new NotImplementedException();
    }
}