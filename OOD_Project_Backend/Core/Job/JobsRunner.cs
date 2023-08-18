using OOD_Project_Backend.Channel.Subscription.Business.Contracts;
using OOD_Project_Backend.Finance.Business.Contracts;

namespace OOD_Project_Backend.Core.Job;

public class JobsRunner : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    
    public JobsRunner(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var deactivationJob = scope.ServiceProvider.GetRequiredService<ISubscriptionDeactivationJob>();
            var refundJob = scope.ServiceProvider.GetRequiredService<IRefundBalanceJob>();
            await deactivationJob.Deactivate();
            await refundJob.Refund();
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}