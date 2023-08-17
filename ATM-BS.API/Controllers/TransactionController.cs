using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ATM_BS.API.Service;
using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;

namespace ATM_BS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        private readonly IBalanceService balanceService;

        public TransactionController(ITransactionService transactionService, IBalanceService balanceService)
        {
            this.transactionService = transactionService;
            this.balanceService = balanceService;
        }

        [HttpPost,Route("AddTransaction")]
        public IActionResult AddTransaction(TransactionDTO transactionDTO)
        {
            try
            {
                Transaction transaction = new Transaction()
                {
                    AccountNumber = transactionDTO.AccountNumber,
                    Region = transactionDTO.Region,
                    Type = transactionDTO.Type,
                    CardNumber = transactionDTO.CardNumber,
                    Amount = transactionDTO.Amount,
                };
                Balance balance = balanceService.GetBalance(transactionDTO.AccountNumber);
                transactionService.AddTransaction(transaction);
                balance.AccountBalance -= transactionDTO.Amount;
                balanceService.EditBalance(balance);
                return StatusCode(200, transactionDTO);
            }
            catch (Exception) { throw; }
        }

        [HttpGet, Route("GetTransactions/{AccountNumber}")]
        public IActionResult GetTransactions(int AccountNumber)
        {
                List<Transaction> transactions = transactionService.GetTransactions(AccountNumber);
                List<TransactionDTO> transactionsDTOs = new List<TransactionDTO>();

                foreach (Transaction transaction in  transactions) {
                    transactionsDTOs.Add(
                        new TransactionDTO()
                        {
                            AccountNumber = transaction.AccountNumber,
                            Type = transaction.Type,
                            CardNumber = transaction.CardNumber,
                            Amount = transaction.Amount,
                            Region = transaction.Region,
                        });
                }
                try
                {
                    return StatusCode(200, transactionsDTOs);
                }
                catch(Exception) { throw;  }
            
        }
    }
}
