using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Finance.Business.Contracts;

public interface WalletService
{
    Task<Response> Withdraw(int amount , int userId);
}