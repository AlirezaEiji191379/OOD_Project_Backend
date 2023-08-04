using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Finance.Business.Contracts;

public interface IFinanceFacade
{
    Task CreateWallet(UserEntity user,int initBalance = 100);
    Task<bool> Buy(int userId, double amount, Dictionary<int, double> incomeShareByUserId);
}