using Microsoft.EntityFrameworkCore;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finanace.DataAccess.Entities.Enums;
using OOD_Project_Backend.Finance.Business.Context;
using OOD_Project_Backend.Finance.Business.Contracts;
using OOD_Project_Backend.Finance.DataAccess.Repository.Contracts;
using OOD_Project_Backend.User.Business.Contracts;

namespace OOD_Project_Backend.Finance.Business.Services;

public class DefaultWalletService : IWalletService
{
    private readonly ITransactionService _transactionService;
    private readonly IWalletRepository _walletRepository;
    private readonly IRefundRepository _refundRepository;
    private readonly IBankService _bankService;
    private readonly IUserFacade _userFacade;

    public DefaultWalletService(ITransactionService transactionService, IWalletRepository walletRepository,
        IRefundRepository refundRepository, IUserFacade userFacade, IBankService bankService)
    {
        _transactionService = transactionService;
        _walletRepository = walletRepository;
        _refundRepository = refundRepository;
        _userFacade = userFacade;
        _bankService = bankService;
    }

    public async Task<Response> GetWallet()
    {
        try
        {
            var userId = _userFacade.GetCurrentUserId();
            var wallet = await _walletRepository.FindByUserId(userId);
            return new Response(200,new {Message = wallet.Balance});
        }
        catch (Exception e)
        {
            return new Response(400,new {Message = "failed to get wallet!"});
        }
    }
    
    public async Task<Response> ChargeWallet(ChargeWalletRequest request)
    {
        
        var userId = _userFacade.GetCurrentUserId();
        var userContract = await _userFacade.GetCurrentUser();
        if (string.IsNullOrEmpty(userContract.CardNumber))
        {
            return new Response(400,new {Message = "you must first add card number!"});
        }
        await using var transaction = await _walletRepository.BeginTransactionAsync();
        try
        {
            var cardNumber = userContract.CardNumber;
            var contract = await _bankService.PayToGhasedakAccount(request.Amount);
            if (!contract.Success)
            {
                throw new Exception("");
            }
            await IncreaseBalance(request.Amount,userId);
            await _transactionService.CreateTransaction(request.Amount,
                userId,
                TransactionType.CHARGE,
                userContract.CardNumber,
                contract.Destination,
                TransactionStatus.COMPLETED);
            await _walletRepository.SaveChangesAsync();
            await transaction.CommitAsync();
            return new Response(200,new {Messge = "Wallet charged successfully!"});
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            await _transactionService.CreateTransaction(request.Amount,userId,TransactionType.CHARGE,userContract.CardNumber,_bankService.GetGhasedakBankAccount(),TransactionStatus.FAILED);
            await _walletRepository.SaveChangesAsync();
            return new Response(400, new { Message = "failed to charge wallet" });
        }
    }
    
    public async Task<Response> Withdraw(WithdrawWalletRequest withdrawWalletRequest)
    {
        var amount = withdrawWalletRequest.Amount;
        await using var transaction = await _walletRepository.BeginTransactionAsync();
        var userContract = await _userFacade.GetCurrentUser();
        try
        {
            await DecreaseBalance(amount,userContract.Id);
            var bankTransaction = await _transactionService.CreateTransaction(amount, userContract.Id, TransactionType.REFUND, _bankService.GetGhasedakBankAccount(), userContract.CardNumber,TransactionStatus.WAITING);
            await _refundRepository.Create(new RefundEntity()
            {
                UserId = userContract.Id,
                Amount = amount,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                Status = RefundStatus.WAITING,
                Transaction = bankTransaction
            });
            await _refundRepository.SaveChangesAsync();
            await transaction.CommitAsync();
            return new Response(200,
                new { Message = "refund created and after 2 day the money goes back to your banck accout" });            
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            await _transactionService.CreateTransaction(amount,userContract.Id,TransactionType.CHARGE,userContract.CardNumber,_bankService.GetGhasedakBankAccount(),TransactionStatus.FAILED);
            await _walletRepository.SaveChangesAsync();
            return new Response(400, new { Message = "withdraw failed!"});
        }
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
            throw new Exception();
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
            throw new Exception();
        }
    }
}