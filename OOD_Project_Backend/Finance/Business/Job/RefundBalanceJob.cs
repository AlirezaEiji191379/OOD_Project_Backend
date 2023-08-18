using OOD_Project_Backend.Finanace.DataAccess.Entities.Enums;
using OOD_Project_Backend.Finance.Business.Contracts;
using OOD_Project_Backend.Finance.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Finance.Business.Job;

public class RefundBalanceJob : IRefundBalanceJob
{
    private readonly IRefundRepository _refundRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IBankService _bankService;

    public RefundBalanceJob(IRefundRepository refundRepository, ITransactionRepository transactionRepository, IBankService bankService)
    {
        _refundRepository = refundRepository;
        _transactionRepository = transactionRepository;
        _bankService = bankService;
    }

    public async Task Refund()
    {
        var refunds = await _refundRepository.FindAllWaitingIncludeTransaction();
        try
        {
            foreach (var refund in refunds)
            {
                var result = await _bankService.PayToUser(refund);
                if (!result)
                {
                    continue;
                }

                refund.Transaction.Status = TransactionStatus.COMPLETED;
                refund.Status = RefundStatus.COMPLETED;
                _transactionRepository.Update(refund.Transaction);
                _refundRepository.Update(refund);
            }
            await _refundRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
        }
    }
}