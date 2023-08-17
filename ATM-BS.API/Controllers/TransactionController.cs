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

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
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
                transactionService.AddTransaction(transaction);
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
