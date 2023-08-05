using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Finance.Business.Contracts;

public interface IFinanceFacade
{
    Task CreateWallet(int userId);
    Task<bool> Buy(int userId, double amount, Dictionary<int, double> incomeShareByUserId);
}