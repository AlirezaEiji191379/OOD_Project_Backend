using OOD_Project_Backend.Core.DataAccess;
using OOD_Project_Backend.Core.DataAccess.Repository;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.User.DataAccess.Repositories;

internal class UserRepository : BaseRepository<UserEntity>
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}