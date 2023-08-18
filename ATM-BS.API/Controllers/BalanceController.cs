using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;
using ATM_BS.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATM_BS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        
        private readonly IBalanceService balanceService;

        public BalanceController(IBalanceService balanceService)
        {
            this.balanceService = balanceService;
        }

        [HttpPost,Route("AddBalance"),Authorize]
        public IActionResult AddBalance(BalanceDTO balanceDTO) { 
            try
            {
                Balance balance = new Balance()
                {
                    AccountNumber = balanceDTO.AccountNumber,
                    AccountBalance = balanceDTO.AccountBalance
                };
                balanceService.AddBalance(balance);
                return StatusCode(200, balanceDTO);
            }
            catch (Exception) { throw; }
        }

        [HttpGet,Route("GetBalance/{accountNumber}"),Authorize]
        public IActionResult GetBalance(int accountNumber)
        {
            try
            {
                Balance balance = balanceService.GetBalance(accountNumber);
                BalanceDTO balanceDTO = new BalanceDTO()
                {
                    AccountNumber = balance.AccountNumber,
                    AccountBalance = balance.AccountBalance
                };
                return StatusCode(200, balanceDTO);
            }
            catch (Exception) { throw; }
        }

        [HttpPut,Route("EditBalance"),Authorize]
        public IActionResult EditBalance(BalanceDTO balanceDTO)
        {
            try
            {
                Balance balance = new Balance()
                {
                    AccountNumber = balanceDTO.AccountNumber,
                    AccountBalance = balanceDTO.AccountBalance
                };
                balanceService.EditBalance(balance);
                return StatusCode(200, balanceDTO);
            }
            catch(Exception) { throw; }
        }

        [HttpPut,Route("ChequeDeposit"),Authorize]
        public IActionResult ChequeDeposit(DepositDTO depositDTO)
        {
            try
            {
                Balance balance = balanceService.GetBalance(depositDTO.AccountNumber);
                balance.AccountBalance += depositDTO.Amount;
                balanceService.EditBalance(balance);
                return StatusCode(200, balance);
            }
            catch(Exception) { throw; }
        }
    }
}
