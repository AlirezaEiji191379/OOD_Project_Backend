using OOD_Project_Backend.Channel.ChannelCore.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Channel.Subscription.Business.Contracts;

namespace OOD_Project_Backend.Channel.Subscription.Business.Jobs;

public class SubscriptionDeactivationJob : ISubscriptionDeactivationJob
{
    private readonly IChannelPremiumUsersRepository _channelPremiumUsersRepository;

    public SubscriptionDeactivationJob(IChannelPremiumUsersRepository channelPremiumUsersRepository)
    {
        _channelPremiumUsersRepository = channelPremiumUsersRepository;
    }

    public async Task Deactivate()
    {
        var deprecatedPremiums = await _channelPremiumUsersRepository.FindAfterDate(DateTime.Now.ToUniversalTime());
        foreach (var deprecatedPremium in deprecatedPremiums)
        {
            deprecatedPremium.Active = false;
            _channelPremiumUsersRepository.Update(deprecatedPremium);
        }
        if (deprecatedPremiums.Any())
        {
            await _channelPremiumUsersRepository.SaveChangesAsync();
        }
    }
}