using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finance.Business.Contracts;
using OOD_Project_Backend.Finance.DataAccess.Repository.Contracts;
using OOD_Project_Backend.User.Business.Contracts;
using OOD_Project_Backend.User.DataAccess.Entities;

namespace OOD_Project_Backend.Finance;

public class FinanceFacade : IFinanceFacade
{
    private readonly IWalletRepository _walletRepository;
    private readonly IWalletService _walletService;
    private readonly IUserFacade _userFacade;
    
    public FinanceFacade(IWalletRepository walletRepository, IWalletService walletService, IUserFacade userFacade)
    {
        _walletRepository = walletRepository;
        _walletService = walletService;
        _userFacade = userFacade;
    }

    public async Task CreateWallet(int userId)
    {
        var wallet = new WalletEntity()
        {
            Balance = 0,
            UserId = userId
        };
        await _walletRepository.Create(wallet);
    }

    public async Task<bool> Buy(int userId, double amount,Dictionary<int,double> incomeShareByUserId)
    {
        try
        {
            await _walletService.DecreaseBalance(amount, userId);
            amount = 0.9 * amount;
            foreach (var adminId in incomeShareByUserId.Keys)
            {
                await _walletService.IncreaseBalance(amount * (incomeShareByUserId[adminId] / 100), adminId);
            }
            await _walletRepository.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}