using OOD_Project_Backend.Channel.DataAccess.Entities;
using OOD_Project_Backend.Channel.DataAccess.Repositories.Contracts;
using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;

namespace OOD_Project_Backend.Channel.DataAccess.Repositories;

public class NonPremiumUsersPremiumContentsRepository : BaseRepository<NonPremiumUsersPremiumContentsEntity>,INonPremiumUsersPremiumContentsRepository
{
    public NonPremiumUsersPremiumContentsRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}