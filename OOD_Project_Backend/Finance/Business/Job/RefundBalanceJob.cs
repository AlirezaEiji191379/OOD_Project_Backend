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
        var parelellOptions = new ParallelOptions()
        {
            MaxDegreeOfParallelism = 10
        };
        await Parallel.ForEachAsync(refunds, parelellOptions, async (refund,CancellationToken) =>
        {
            var result = await _bankService.PayToUser(refund);
            if (!result)
            {
                return;
            }

            refund.Transaction.Status = TransactionStatus.COMPLETED;
            refund.Status = RefundStatus.COMPLETED;
            _transactionRepository.Update(refund.Transaction);
            _refundRepository.Update(refund);
            await _refundRepository.SaveChangesAsync();
        });

    }
}