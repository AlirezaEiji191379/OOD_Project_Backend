using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Finanace.Business.Abstractions;

public interface WalletService
{
    Task<Response> Withdraw(int amount , int userId);
}