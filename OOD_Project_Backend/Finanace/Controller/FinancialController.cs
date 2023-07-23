using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Common.Response;
using OOD_Project_Backend.Finanace.Business.Abstractions;

namespace OOD_Project_Backend.Finanace.Controller;

[ApiController]
[Route("Wallet")]
public class FinancialController : ControllerBase
{
    private readonly WalletService _walletService;

    public FinancialController(WalletService walletService)
    {
        _walletService = walletService;
    }


    [HttpPut]
    [Route("Withdraw/{amount}")]
    [Authorize]
    public async Task<Response> Withdraw(int amount)
    {
        var userId = (int)HttpContext.Items["User"];
        return await _walletService.Withdraw(amount, userId);
    }
}