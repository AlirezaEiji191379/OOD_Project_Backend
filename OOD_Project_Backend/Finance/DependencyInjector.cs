using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finanace.DataAccess.Repository;
using OOD_Project_Backend.Finance;
using OOD_Project_Backend.Finance.Business.Contracts;
using OOD_Project_Backend.Finance.Business.Services;
using OOD_Project_Backend.Finance.DataAccess.Repository.Contracts;

namespace OOD_Project_Backend.Finanace.DiManager;

public class DependencyInjector : IDependencyInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddScoped<IRefundRepository, RefundRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<ITransactionService,DefaultTransactionService>();
        services.AddScoped<IWalletService,DefaultWalletService>();
        services.AddScoped<IFinanceFacade, FinanceFacade>();
        services.AddScoped<IBankService, BankService>();
    }
}