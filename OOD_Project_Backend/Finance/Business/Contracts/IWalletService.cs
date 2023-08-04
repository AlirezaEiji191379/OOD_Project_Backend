﻿using OOD_Project_Backend.Core.Context;

namespace OOD_Project_Backend.Finance.Business.Contracts;

public interface IWalletService
{
    Task<Response> Withdraw(int amount);
    Task<bool> DecreaseBalance(double amount, int userId);
    Task<bool> IncreaseBalance(double amount, int userId);
}