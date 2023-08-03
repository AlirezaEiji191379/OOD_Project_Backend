﻿using OOD_Project_Backend.Core.Common.DependencyInjection.Abstractions;
using OOD_Project_Backend.Core.DataAccess.Contracts;
using OOD_Project_Backend.Finanace.Business.Abstractions;
using OOD_Project_Backend.Finanace.Business.Services;
using OOD_Project_Backend.Finanace.DataAccess.Entities;
using OOD_Project_Backend.Finanace.DataAccess.Repository;
using OOD_Project_Backend.Finanace.Facade;
using OOD_Project_Backend.Finanace.Facade.Abstractions;

namespace OOD_Project_Backend.Finanace.DiManager;

public class FinanaceDiManager : IDependencyInstaller
{
    public void Install(IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<RefundEntity>, RefundRepository>();
        services.AddScoped<IBaseRepository<TransactionEntity>, TransactionRepository>();
        services.AddScoped<IBaseRepository<WalletEntity>, WalletRepository>();
        services.AddScoped<TransactionService,DefaultTransactionService>();
        services.AddScoped<WalletService,DefaultWalletService>();
        services.AddScoped<IFinanaceFacade, FinanaceFacade>();
    }
}