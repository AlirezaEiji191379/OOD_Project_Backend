using Microsoft.AspNetCore.Mvc;
using OOD_Project_Backend.Core.Common.Authentication;
using OOD_Project_Backend.Core.Context;
using OOD_Project_Backend.Finance.Business.Context;
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
    [Route("Withdraw")]
    [Authorize]
    public async Task<Response> Withdraw(WithdrawWalletRequest withdrawWalletRequest)
    {
        return await _walletService.Withdraw(withdrawWalletRequest);
    }

    [HttpPut]
    [Route("ChargeWallet")]
    [Authorize]
    public async Task<Response> ChargeWallet([FromBody] ChargeWalletRequest chargeWalletRequest)
    {
        return await _walletService.ChargeWallet(chargeWalletRequest);
    }

    [HttpGet]
    [Route("GetWallet")]
    [Authorize]
    public async Task<Response> GetWallet()
    {
        return await _walletService.GetWallet();
    }
}