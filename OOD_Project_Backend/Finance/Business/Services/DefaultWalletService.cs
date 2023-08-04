using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finanace.DataAccess.Entities.Enums;
using OOD_Project_Backend.Finance.Business.Contracts;
using OOD_Project_Backend.Finance.DataAccess.Repository.Contracts;
using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.Finance.Business.Services;

public class DefaultWalletService : IWalletService
{
    private readonly ITransactionService _transactionService;
    private readonly IWalletRepository _walletRepository;
    private readonly IBaseRepository<RefundEntity> _refundRepository;
    private readonly IUserFacade _userFacade;

    public DefaultWalletService(ITransactionService transactionService, IWalletRepository walletRepository,
        IBaseRepository<RefundEntity> refundRepository, IUserFacade userFacade)
    {
        _transactionService = transactionService;
        _walletRepository = walletRepository;
        _refundRepository = refundRepository;
        _userFacade = userFacade;
    }

    public async Task<Response> Withdraw(int amount)
    {
        var userId = _userFacade.GetCurrentUserId();
        var wallet = await _walletRepository.FindByCondition(x => x.UserId == userId, true)
            .FirstOrDefaultAsync();
        if (wallet.Balance >= amount)
        {
            wallet.Balance -= amount;
            _walletRepository.Update(wallet);
            var transaction =
                await _transactionService.CreateTransaction(amount, userId, TransactionType.REFUND, "", "");
            await _refundRepository.Create(new RefundEntity()
            {
                UserId = userId,
                Amount = amount,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                Status = RefundStatus.WAITING,
                Transaction = transaction
            });
            await _refundRepository.SaveChangesAsync();
            return new Response(200,
                new { Message = "refund crated and after 2 day the money goes back to your banck accout" });
        }

        return new Response(200, new { Message = "you can not witdraw more than your wallet" });
    }

    public async Task<bool> IncreaseBalance(double amount, int userId)
    {
        try
        {
            var wallet = await _walletRepository.FindByUserId(userId);
            wallet.Balance += amount;
            _walletRepository.Update(wallet);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> DecreaseBalance(double amount, int userId)
    {
        try
        {
            var wallet = await _walletRepository.FindByUserId(userId);
            if (wallet.Balance >= amount)
            {
                wallet.Balance -= amount;
                _walletRepository.Update(wallet);
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}