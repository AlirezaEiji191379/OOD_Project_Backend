using OOD_Project_Backend.Finanace.DataAccess.Entities.Enums;

namespace OOD_Project_Backend.Finanace.Business.Abstractions;

public interface TransactionService
{
    Task CreateTransaction(int amount,int userId,TransactionType type,string src,string dest);
}