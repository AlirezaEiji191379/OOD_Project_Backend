using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.User.Business.Context;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.User.DataAccess.Repositories.Contract;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    Task<UserEntity> FindByUserId(int userId, bool trackChange);
    Task<UserContract> GetUserProfile(int userId);
}