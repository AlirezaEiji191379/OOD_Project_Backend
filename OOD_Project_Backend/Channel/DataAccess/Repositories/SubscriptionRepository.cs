using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories;

public class SubscriptionRepository : BaseRepository<SubscriptionEntity>,ISubscriptionRepository
{
    public SubscriptionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}