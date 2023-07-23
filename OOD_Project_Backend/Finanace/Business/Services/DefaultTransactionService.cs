using OOD_Project_Backend.Core.DataAccess.Abstractions;
using OOD_Project_Backend.Finanace.Business.Abstractions;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finanace.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Finanace.Business.Services;

public class DefaultTransactionService : TransactionService
{
    private readonly IBaseRepository<TransactionEntity> _trasactionBaseRepository;

    public DefaultTransactionService(IBaseRepository<TransactionEntity> trasactionBaseRepository)
    {
        _trasactionBaseRepository = trasactionBaseRepository;
    }

    public async Task<TransactionEntity> CreateTransaction(int amount, int userId, TransactionType type, string src, string dest)
    {
        var transaction = new TransactionEntity
        {
            BankToken = Guid.NewGuid().ToString(),
            Amount = amount,
            UserId = userId,
            Destination = dest,
            Source = src,
            Status = TransactionStatus.WAITING,
            CreatedAt = DateTime.Now.ToUniversalTime()
        };
        await _trasactionBaseRepository.Create(transaction);
        await _trasactionBaseRepository.SaveChangesAsync();
        return transaction;
    }
    
}