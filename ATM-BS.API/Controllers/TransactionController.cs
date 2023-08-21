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

        [HttpPost, Route("AddTransaction")]
        public IActionResult AddTransaction(TransactionDTO transactionDTO)
        {

            try
            {
                if (transactionDTO.FromAccountNumber == null && transactionDTO.ToAccountNumber == null)
                {
                    throw new Exception("Invalid transaction");
                }

                //Double val = 0;
                Balance fromAccountBalance = new Balance();
                Balance toAccountBalance = new Balance();
                /*if (transactionDTO.FromAccountNumber != null)
                {
                    FromAccountBalance = balanceService.GetBalance(transactionDTO.FromAccountNumber.Value);
                    val = FromAccountBalance.AccountBalance;

                    val -= transactionDTO.Amount;
                    if (val < 0)
                    {
                        throw new Exception("Insufficent Balance!");
                    }
                }
                if (transactionDTO.ToAccountNumber != null)
                {
                    ToAccountBalance = balanceService.GetBalance(transactionDTO.ToAccountNumber.Value);
                }

                if (transactionDTO.FromAccountNumber != null)
                {
                    FromAccountBalance.AccountBalance -= transactionDTO.Amount;
                    balanceService.EditBalance(FromAccountBalance);
                }
                if (transactionDTO.ToAccountNumber != null)
                {
                    ToAccountBalance.AccountBalance += transactionDTO.Amount;
                    balanceService.EditBalance(ToAccountBalance);
                }*/
                if(transactionDTO.ToAccountNumber == null)
                {
                    fromAccountBalance = balanceService.GetBalance(transactionDTO.FromAccountNumber.Value);
                    double val = fromAccountBalance.AccountBalance;
                    if(val < transactionDTO.Amount)
                    {
                        throw new Exception("Insufficient balance");
                    }
                    fromAccountBalance.AccountBalance -= transactionDTO.Amount;
                    balanceService.EditBalance(fromAccountBalance);
                }
                else if(transactionDTO.FromAccountNumber == null) {
                    toAccountBalance = balanceService.GetBalance(transactionDTO.ToAccountNumber.Value);
                    toAccountBalance.AccountBalance += transactionDTO.Amount;
                    balanceService.EditBalance(toAccountBalance);
                }
                else
                {
                    fromAccountBalance = balanceService.GetBalance(transactionDTO.FromAccountNumber.Value);
                    toAccountBalance = balanceService.GetBalance(transactionDTO.ToAccountNumber.Value);
                    fromAccountBalance.AccountBalance -= transactionDTO.Amount;
                    toAccountBalance.AccountBalance += transactionDTO.Amount;
                    balanceService.EditBalance(fromAccountBalance);
                    balanceService.EditBalance(toAccountBalance);
                }



                Transaction transaction = new Transaction()
                {
                    TransactionId = Guid.NewGuid(),
                    FromAccountNumber = transactionDTO.FromAccountNumber,
                    ToAccountNumber = transactionDTO.ToAccountNumber,
                    Amount = transactionDTO.Amount,
                    FromAccountBalance = fromAccountBalance.AccountBalance,
                    ToAccountBalance = toAccountBalance.AccountBalance,

                    /*
                    FromAccountNumber = (transactionDTO.FromAccountNumber!=null) ? transactionDTO.FromAccountNumber : null,
                    ToAccountNumber = (transactionDTO.ToAccountNumber != null) ? transactionDTO.ToAccountNumber : null,
                    FromAccountBalance = (transactionDTO.FromAccountNumber != null) ? FromAccountBalance.AccountBalance : null,
                    ToAccountBalance = (transactionDTO.FromAccountNumber != null) ? ToAccountBalance.AccountBalance : null,
                    */


                };
                // 
                transactionService.AddTransaction(transaction);
                TransactionDisplayDTO transactionDisplayDTO = _mapper.Map<TransactionDisplayDTO>(transaction);
                return StatusCode(200, transactionDisplayDTO);
            }
            catch (Exception) { throw; }
        }

        [HttpGet, Route("GetTransactions/{AccountNumber}"), Authorize]
        public IActionResult GetTransactions(int AccountNumber)
        {


            /*foreach (Transaction transaction in  transactions) {
                /*transactionsDTOs.Add(
                    new TransactionDTO()
                    {
                        AccountNumber = transaction.AccountNumber,
                        Type = transaction.Type,
                        CardNumber = transaction.CardNumber,
                        Amount = transaction.Amount,
                        Region = transaction.Region,
                    });
            TransactionDisplayDTO transactionDisplayDTO = _mapper.Map<List<TransactionDisplayDTO>>(transaction);
        }*/
            try
            {
                List<Transaction> transactions = transactionService.GetTransactions(AccountNumber);
                List<TransactionDisplayDTO> transactionsDisplayDTOs = new List<TransactionDisplayDTO>();
                transactionsDisplayDTOs = _mapper.Map<List<TransactionDisplayDTO>>(transactions);

                return StatusCode(200, transactionsDisplayDTOs);
            }
            catch (Exception) { throw; }

        }
    }
}
