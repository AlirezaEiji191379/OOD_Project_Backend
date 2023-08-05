using OOD_Project_Backend.Finanace.DataAccess.Entities;

namespace OOD_Project_Backend.Finance.Business.Contracts;

public interface IBankService
{
    Task<TransactionContract> PayToGhasedakAccount(double amount);
    Task<bool> PayToUser(RefundEntity refundEntity);
    string GetGhasedakBankAccount();
}