using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finanace.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Finance.Business.Contracts;

public interface ITransactionService
{
    Task<TransactionEntity> CreateTransaction(int amount, int userId, TransactionType type, string src, string dest);
}