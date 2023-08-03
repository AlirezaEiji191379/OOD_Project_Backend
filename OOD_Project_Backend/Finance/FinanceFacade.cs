using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finance.Business.Contracts;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Finance;

public class FinanceFacade : IFinanceFacade
{
    private readonly IBaseRepository<WalletEntity> _walletRepository;

    public FinanceFacade(IBaseRepository<WalletEntity> walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task CreateWallet(UserEntity user, int initBalance = 100)
    {
        var wallet = new WalletEntity()
        {
            Balance = initBalance,
            User = user
        };
        await _walletRepository.Create(wallet);
    }

    public async Task<bool> Buy(int userId, int amount,Dictionary<int,double> incomeShareByUserId)
    {
        return false;
    }
}