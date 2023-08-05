namespace OOD_Project_Backend.Finance.Business.Contracts;

public interface IBankService
{
    Task<TransactionContract> PayToGhasedakAccount(double amount);
    string GetGhasedakBankAccount();
}