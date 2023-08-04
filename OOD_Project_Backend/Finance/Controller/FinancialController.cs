using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Finance.Business.Contracts;

namespace OOD_Project_Backend.Finanace.Controller;

[ApiController]
[Route("Wallet")]
public class FinancialController : ControllerBase
{
    private readonly IWalletService _walletService;

    public FinancialController(IWalletService walletService)
    {
        _walletService = walletService;
    }


    [HttpPut]
    [Route("Withdraw/{amount}")]
    [Authorize]
    public async Task<Response> Withdraw(int amount)
    {
        var userId = (int)HttpContext.Items["User"];
        return await _walletService.Withdraw(amount);
    }
}