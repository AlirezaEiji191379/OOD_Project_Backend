﻿using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Core.DataAccess.Contracts;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;

public interface ISubscriptionRepository : IBaseRepository<SubscriptionEntity>
{
    Task<List<SubscriptionEntity>> FindByChannelId(int channelId);
    Task<SubscriptionEntity> FindById(int subscriptionId);
}