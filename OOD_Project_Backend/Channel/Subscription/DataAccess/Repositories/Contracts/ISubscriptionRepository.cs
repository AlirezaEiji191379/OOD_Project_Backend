using OOD_Project_Backend.Channel.Subscription.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Channel.Subscription.DataAccess.Repositories.Contracts;

public interface ISubscriptionRepository : IBaseRepository<SubscriptionEntity>
{
    Task<List<SubscriptionEntity>> FindByChannelId(int channelId);
    Task<SubscriptionEntity> FindById(int subscriptionId);
}