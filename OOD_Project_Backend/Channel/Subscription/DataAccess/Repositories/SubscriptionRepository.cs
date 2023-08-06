using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Channel.Subscription.DataAccess.Entities;
using OOD_Project_Backend.Channel.Subscription.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Channel.Subscription.DataAccess.Repositories;

public class SubscriptionRepository : BaseRepository<SubscriptionEntity>,ISubscriptionRepository
{
    private readonly AppDbContext _dbContext;
    public SubscriptionRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<SubscriptionEntity>> FindByChannelId(int channelId)
    {
        return _dbContext.Subscriptions.Where(x => x.ChannelId == channelId).ToListAsync();
    }

    public Task<SubscriptionEntity> FindById(int subscriptionId)
    {
        return _dbContext
            .Subscriptions
            .Where(x => x.Id == subscriptionId)
            .SingleOrDefaultAsync();
    }
}