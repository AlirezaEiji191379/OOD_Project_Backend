using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Abstractions;
using OOD_Project_Backend.Finanace.Business.Abstractions;
using OOD_Project_Backend.Finanace.Business.Services;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finanace.DataAccess.Repository;
using OOD_Project_Backend.Finanace.Facade;
using OOD_Project_Backend.Finanace.Facade.Abstractions;

namespace OOD_Project_Backend.Finanace.DiManager;

public class FinanaceDiManager : IDependencyInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBaseRepository<RefundEntity>, RefundRepository>();
        serviceCollection.AddScoped<IBaseRepository<TransactionEntity>, TransactionRepository>();
        serviceCollection.AddScoped<IBaseRepository<WalletEntity>, WalletRepository>();
        serviceCollection.AddScoped<TransactionService,DefaultTransactionService>();
        serviceCollection.AddScoped<WalletService,DefaultWalletService>();
        serviceCollection.AddScoped<IFinanaceFacade, FinanaceFacade>();
    }
}