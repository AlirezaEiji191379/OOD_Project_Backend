using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Core.Common.Response;
using OOD_Project_Backend.Core.DataAccess.Abstractions;
using OOD_Project_Backend.Finanace.Business.Abstractions;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finanace.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Finanace.Business.Services;

public class DefaultWalletService : WalletService
{
    private readonly TransactionService _transactionService;
    private readonly IBaseRepository<WalletEntity> _walletRepository;
    private readonly IBaseRepository<RefundEntity> _refundRepository;
    public DefaultWalletService(TransactionService transactionService, IBaseRepository<WalletEntity> walletRepository, IBaseRepository<RefundEntity> refundRepository)
    {
        _transactionService = transactionService;
        _walletRepository = walletRepository;
        _refundRepository = refundRepository;
    }

    public async Task<Response> Withdraw(int amount,int userId)
    {
        var wallet = await _walletRepository.FindByCondition(x => x.UserId == userId,true)
            .FirstOrDefaultAsync();
        if (wallet.Balance >= amount) {
            wallet.Balance -= amount;
            _walletRepository.Update(wallet);
            var transaction= await _transactionService.CreateTransaction(amount, userId, TransactionType.REFUND, "", "");
            await _refundRepository.Create(new RefundEntity()
            {
                UserId = userId,
                Amount = amount,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                Status = RefundStatus.WAITING,
                Transaction = transaction
            });
            await _refundRepository.SaveChangesAsync();
            return new Response(200,new {Message = "refund crated and after 2 day the money goes back to your banck accout"});    
        }
        return new Response(200,new {Message = "you can not witdraw more than your wallet"});
    }
}