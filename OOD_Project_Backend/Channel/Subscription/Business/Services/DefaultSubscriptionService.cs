using OOD_Project_Backend.Channel.ChannelCore.Business.Contracts;
using OOD_Project_Backend.Channel.ChannelCore.Business.Validations.Conditions;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Entities;
using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Channel.Subscription.Business.Context;
using OOD_Project_Backend.Channel.Subscription.Business.Contracts;
using OOD_Project_Backend.Channel.Subscription.DataAccess.Entities;
using OOD_Project_Backend.Channel.Subscription.DataAccess.Entities.Enums;
using OOD_Project_Backend.Channel.Subscription.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Content.ContentCore.Business.Contracts;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Core.Validation.Contracts;
using OOD_Project_Backend.Finance.Business.Contracts;
using OOD_Project_Backend.User.Business.Contracts;
using Exception = System.Exception;

namespace OOD_Project_Backend.Channel.Subscription.Business.Services;

public class DefaultSubscriptionService : ISubscriptionService
{
    private readonly IUserFacade _userFacade;
    private readonly IChannelMembershipService _channelMembershipService;
    private readonly IValidator _validator;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IChannelMemberRepository _channelMemberRepository;
    private readonly IChannelPremiumUsersRepository _channelPremiumUsersRepository;
    private readonly IFinanceFacade _financeFacade;
    private readonly IContentFacade _contentFacade;
    private readonly INonPremiumUsersPremiumContentsRepository _contentsRepository;

    public DefaultSubscriptionService(IUserFacade userFacade,
        IChannelMembershipService channelMembershipService,
        IValidator validator,
        ISubscriptionRepository subscriptionRepository,
        IChannelMemberRepository channelMemberRepository,
        IFinanceFacade financeFacade,
        IChannelPremiumUsersRepository channelPremiumUsersRepository,
        IContentFacade contentFacade,
        INonPremiumUsersPremiumContentsRepository contentsRepository)
    {
        _userFacade = userFacade;
        _channelMembershipService = channelMembershipService;
        _validator = validator;
        _subscriptionRepository = subscriptionRepository;
        _channelMemberRepository = channelMemberRepository;
        _financeFacade = financeFacade;
        _channelPremiumUsersRepository = channelPremiumUsersRepository;
        _contentFacade = contentFacade;
        _contentsRepository = contentsRepository;
    }


    public async Task<bool> CheckContentToShowUser(int userId, int channelId, int contentId)
    {
        try
        {
            var nonPremiumUserContent = await _contentsRepository.Find(contentId, userId);
            var premiumUser = await _channelPremiumUsersRepository.Find(userId, channelId);
            var membership = await _channelMemberRepository.FindByUserIdAndChannelId(userId, channelId);
            return membership != null && (nonPremiumUserContent != null || premiumUser != null);
        }
        catch (Exception e)
        {
            return false;
        }
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
            return new Response(400, new { Message = "duplicated subscription!" });
        }
    }

    public async Task<Response> EditSubscription(SubscriptionUpdateRequest request)
    {
        try
        {
            if (!_validator.Validate(new PriceRule(request.Price)))
            {
                return new Response(400, new { Message = "invalid subscription request!" });
            }

            var subscription = await _subscriptionRepository.FindById(request.SubscriptionId);
            var userId = _userFacade.GetCurrentUserId();
            if (await _channelMembershipService.IsOwner(subscription.ChannelId, userId) == false)
            {
                return new Response(403, new { Message = "only owners can update subscriptions!" });
            }
            subscription.Price = request.Price;
            _subscriptionRepository.Update(subscription);
            await _subscriptionRepository.SaveChangesAsync();
            return new Response(200,new {Message = "subscription updated successfully!"});
        }
        catch (Exception e)
        {
            return new Response(400,new {Message = "subscription update failed!"});
        }
    }

    public async Task<Response> ShowSubscription(int channelId)
    {
        try
        {
            var subEntities = await _subscriptionRepository.FindByChannelId(channelId);
            var subDtos = subEntities.Select(x => new
            {
                Id = x.Id,
                ChannelId = x.ChannelId,
                Period = x.Period,
                Price = x.Price
            }).ToList();
            return new Response(200, new { Message = subDtos });
        }
        catch (Exception e)
        {
            return new Response(404, new { Message = "channel not found!" });
        }
    }

    public async Task<Response> BuySubscription(int subscriptionId)
    {
        await using var transaction = await _subscriptionRepository.BeginTransactionAsync();
        try
        {
            var subscription = await _subscriptionRepository.FindById(subscriptionId);
            if (subscription == null)
            {
                throw new Exception("subscription not found!");
            }

            var amount = subscription.Price;
            var userId = _userFacade.GetCurrentUserId();
            var members = await _channelMemberRepository.FindByChannelId(subscription.ChannelId);
            var incomeShares = members.Where(x => x.IncomeShare > 0).ToDictionary(x => x.UserId, x => x.IncomeShare);
            var buyResult = await _financeFacade.Buy(userId, amount, incomeShares);
            if (!buyResult)
            {
                throw new Exception("buy failed!");
            }

            var startTime = DateTime.Now.ToUniversalTime();
            var extraMonth = ComputeExtraMonth(subscription);
            var premiumUserEntity = new ChannelPremiumUsersEntity()
            {
                ChannelId = subscription.ChannelId,
                Active = true,
                UserId = userId,
                StartTime = startTime,
                EndTime = startTime.AddMonths(extraMonth)
            };
            await _channelPremiumUsersRepository.Create(premiumUserEntity);
            await _channelPremiumUsersRepository.SaveChangesAsync();
            await transaction.CommitAsync();
            return new Response(200, new { Message = "buy successfull!" });
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return new Response(400, new { Message = "the subscription was bougth or another error occured!" });
        }
    }

    public async Task<Response> BuyContent(int contentId)
    {
        await using var transaction = await _subscriptionRepository.BeginTransactionAsync();
        try
        {
            var contentDetails = await _contentFacade.GetContentDetails(contentId);

            var amount = contentDetails.Price;
            var userId = _userFacade.GetCurrentUserId();
            var members = await _channelMemberRepository.FindByChannelId(contentDetails.ChannelId);
            var incomeShares = members.Where(x => x.IncomeShare > 0).ToDictionary(x => x.UserId, x => x.IncomeShare);
            var buyResult = await _financeFacade.Buy(userId, amount, incomeShares);
            if (!buyResult)
            {
                throw new Exception("buy failed!");
            }

            var nonPremiumContentUser = new NonPremiumUsersPremiumContentsEntity()
            {
                UserId = userId,
                ContentId = contentId
            };
            await _contentsRepository.Create(nonPremiumContentUser);
            await _contentsRepository.SaveChangesAsync();
            await transaction.CommitAsync();
            return new Response(200, new { Message = "buy successfull!" });
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return new Response(400, new { Message = e.Message });
        }
    }

    private static int ComputeExtraMonth(SubscriptionEntity subscription)
    {
        var extraMonth = 0;
        if (subscription.Period == SubscriptionPeriod.MONTH_3)
        {
            extraMonth = 3;
        }
        else if (subscription.Period == SubscriptionPeriod.MONTH_6)
        {
            extraMonth = 6;
        }
        else
        {
            extraMonth = 12;
        }

        return extraMonth;
    }
}