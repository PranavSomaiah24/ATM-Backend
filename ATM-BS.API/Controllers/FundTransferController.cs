using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;
using ATM_BS.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATM_BS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FundTransferController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IBalanceService balanceService;

        public FundTransferController(ICustomerService customerService, IBalanceService balanceService)
        {
            this.customerService = customerService;
            this.balanceService = balanceService;
        }

        [HttpPut, Route("FundTransfer"), Authorize]
        public IActionResult FundTransfer(FundTransferDTO fundTransferDTO)
        {

            try
            {
                Balance FromAccountBalance = balanceService.GetBalance(fundTransferDTO.FromAccountNumber);
                Balance ToAccountBalance = balanceService.GetBalance(fundTransferDTO.ToAccountNumber);

                if (fundTransferDTO.Amount <= FromAccountBalance.AccountBalance)
                {
                    FromAccountBalance.AccountBalance -= fundTransferDTO.Amount;
                    ToAccountBalance.AccountBalance += fundTransferDTO.Amount;
                    balanceService.EditBalance(FromAccountBalance);
                    balanceService.EditBalance(ToAccountBalance);
                }
                else
                {
                    throw new Exception(nameof(fundTransferDTO));
                }
                return StatusCode(200, fundTransferDTO);

            }
            catch (Exception) { throw; }
        
        }
    }
}
