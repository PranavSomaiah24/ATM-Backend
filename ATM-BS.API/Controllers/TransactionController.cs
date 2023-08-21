using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ATM_BS.API.Service;
using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace ATM_BS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        private readonly IBalanceService balanceService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, IBalanceService balanceService, IMapper mapper)
        {
            this.transactionService = transactionService;
            this.balanceService = balanceService;
            this._mapper = mapper;
        }

        [HttpPost,Route("AddTransaction")]
        public IActionResult AddTransaction(TransactionDTO transactionDTO)
        {
            try
            {
                 /* Transaction transaction = new Transaction()
                {
                    AccountNumber = transactionDTO.AccountNumber,
                    Region = transactionDTO.Region,
                    Type = transactionDTO.Type,
                    CardNumber = transactionDTO.CardNumber,
                    Amount = transactionDTO.Amount,
                }; */

               Transaction transaction = _mapper.Map<Transaction>(transactionDTO);
                transactionService.AddTransaction(transaction);
                Balance balance = balanceService.GetBalance(transactionDTO.AccountNumber);
                
                double val = balance.AccountBalance;
                
                val -= transactionDTO.Amount;
                if(val< 0)
                {
                    throw new Exception("Insufficent Balance!");
                }
                balance.AccountBalance -= transactionDTO.Amount;
                balanceService.EditBalance(balance);
                return StatusCode(200, transactionDTO);
            }
            catch (Exception) { throw; }
        }

        [HttpGet, Route("GetTransactions/{AccountNumber}"),Authorize]
        public IActionResult GetTransactions(int AccountNumber)
        {
                List<Transaction> transactions = transactionService.GetTransactions(AccountNumber);
                List<TransactionDTO> transactionsDTOs = new List<TransactionDTO>();

                //foreach (Transaction transaction in  transactions) {
                    //transactionsDTOs.Add(
                         /* new TransactionDTO()
                         {
                             AccountNumber = transaction.AccountNumber,
                             Type = transaction.Type,
                             CardNumber = transaction.CardNumber,
                             Amount = transaction.Amount,
                             Region = transaction.Region,
                         });*/
                        transactionsDTOs = _mapper.Map<List<TransactionDTO>>(transactions);
            //}
                try
                {
                    return StatusCode(200, transactionsDTOs);
                }
                catch(Exception) { throw;  }
            
        }
    }
}
