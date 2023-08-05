﻿using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finanace.DataAccess.Entities.Enums;
using OOD_Project_Backend.Finance.Business.Contracts;

namespace OOD_Project_Backend.Finance.Business.Services;

public class DefaultTransactionService : ITransactionService
{
    private readonly IBaseRepository<TransactionEntity> _trasactionBaseRepository;

    public DefaultTransactionService(IBaseRepository<TransactionEntity> trasactionBaseRepository)
    {
        _trasactionBaseRepository = trasactionBaseRepository;
    }

    public async Task<TransactionEntity> CreateTransaction(double amount, int userId, TransactionType type, string src, string dest,TransactionStatus status = TransactionStatus.WAITING)
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
        return transaction;
    }
    
}