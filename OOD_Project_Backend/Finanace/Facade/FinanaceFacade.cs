using OOD_Project_Backend.Core.DataAccess.Abstractions;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finanace.Facade.Abstractions;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Finanace.Facade;

public class FinanaceFacade : IFinanaceFacade
{
    private readonly IBaseRepository<WalletEntity> _walletRepository;

    public FinanaceFacade(IBaseRepository<WalletEntity> walletRepository)
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
}