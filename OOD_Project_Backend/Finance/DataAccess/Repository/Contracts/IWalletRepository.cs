using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Finanace.DataAccess.Entities;

namespace OOD_Project_Backend.Finance.DataAccess.Repository.Contracts;

public interface IWalletRepository : IBaseRepository<WalletEntity>
{
    Task<WalletEntity> FindByUserId(int userId);
}