using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Finance.Business.Context;

namespace OOD_Project_Backend.Finance.Business.Contracts;

public interface IWalletService
{
    Task<Response> Withdraw(WithdrawWalletRequest withdrawWalletRequest);
    Task<bool> DecreaseBalance(double amount, int userId);
    Task<bool> IncreaseBalance(double amount, int userId);
    Task<Response> ChargeWallet(ChargeWalletRequest request);
    Task<Response> GetWallet();
}