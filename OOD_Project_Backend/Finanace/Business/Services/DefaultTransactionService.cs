using OOD_Project_Backend.Finanace.Business.Abstractions;
using OOD_Project_Backend.Finanace.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Finanace.Business.Services;

public class DefaultTransactionService : TransactionService
{
    
    public Task CreateTransaction(int amount, int userId, TransactionType type, string src, string dest)
    {
        throw new NotImplementedException();
    }
    
}