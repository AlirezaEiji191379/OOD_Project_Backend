using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finance.Business.Contracts;

namespace OOD_Project_Backend.Finance.Business.Services;

public class BankService : IBankService
{
    private const string Destination = "5555000011112222";
    public async Task<TransactionContract> PayToGhasedakAccount(double amount)
    {
        await Task.Delay(4000);
        var mockRandom = new Random().Next(0, 100);
        if (mockRandom < 2)
        {
            return new TransactionContract()
            {
                Destination = Destination,
                Success = false,
                DateTime = DateTime.Now.ToUniversalTime()
            };
        }
        return new TransactionContract()
        {
            Destination = Destination,
            Success = true,
            DateTime = DateTime.Now.ToUniversalTime()
        };
    }

    public async Task<bool> PayToUser(RefundEntity refundEntity)
    {
        await Task.Delay(4000);
        var mockRandom = new Random().Next(0, 100);
        if (mockRandom < 2)
        {
            return false;
        }
        return true;
    }

    public string GetGhasedakBankAccount()
    {
        return Destination;
    }
}