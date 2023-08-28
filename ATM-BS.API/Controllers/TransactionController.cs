using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;
using ATM_BS.API.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATM_BS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        private readonly IBalanceService balanceService;
        private readonly IMapper _mapper;

        class TransactionException : Exception 
        {
            public TransactionException()
            {
            }

            public TransactionException(string message) : base(message) { }
            public override string Message
            {
                get { return "Transaction Failed"; }
            }
            public string GetErrMessage
            {
                get { return "Failed to Fetch Transaction(s)"; }
            }
            public string ChequeErrMessage
            {
                get { return "Failed to Fetch Cheque(s)"; }
            }
        }

        public TransactionController(ITransactionService transactionService, IBalanceService balanceService, IMapper mapper)
        {
            this.transactionService = transactionService;
            this.balanceService = balanceService;
            this._mapper = mapper;
        }

        [HttpPost,Route("AddTransaction"),Authorize]
        public IActionResult AddTransaction(TransactionDTO transactionDTO)
        {

            try
            {
                if (transactionDTO.FromAccountNumber == null && transactionDTO.ToAccountNumber == null)
                {
                    throw new TransactionException();
                }

                //Double val = 0;
                Balance fromAccountBalance = new Balance();
                Balance toAccountBalance = new Balance();
                
                if (transactionDTO.ToAccountNumber == null)
                {
                    fromAccountBalance = balanceService.GetBalance(transactionDTO.FromAccountNumber.Value);
                    double val = fromAccountBalance.AccountBalance;
                    if (val < transactionDTO.Amount)
                    {
                        throw new TransactionException("Insufficient balance");
                    }
                    fromAccountBalance.AccountBalance -= transactionDTO.Amount;
                    balanceService.EditBalance(fromAccountBalance);
                }
                else if (transactionDTO.FromAccountNumber == null)
                {
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
                    TransactionTime = DateTime.Now,


                };
                // 
                transactionService.AddTransaction(transaction);
                //TransactionDisplayDTO transactionDisplayDTO = _mapper.Map<TransactionDisplayDTO>(transaction);
                return StatusCode(200, transaction);
            }
            catch(TransactionException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet, Route("GetAllTransactions/{AccountNumber}"), Authorize]
        public IActionResult GetAllTransactions(int AccountNumber)
        {
            try
            {
                List<Transaction> transactions = transactionService.GetTransactions(AccountNumber);

                return StatusCode(200, transactions);
            }
            catch (TransactionException ex) { return StatusCode(400, ex.GetErrMessage); }

        }

        [HttpGet,Route("GetTransactions/{AccountNumber}"),Authorize]
        public IActionResult GetTransactions(int AccountNumber)
        {
            try
            {
                List<Transaction> transactions = transactionService.GetTransactions(AccountNumber);

                return StatusCode(200, transactions.Skip(transactions.Count - 5));
            }
            catch (TransactionException ex) { return StatusCode(400, ex.GetErrMessage); }

        }

        [HttpGet,Route("GetTransactionsForPeriod"),Authorize]
        public IActionResult GetTransactionsForPeriod(TransactionPeriodDTO transactionPeriodDTO)
        {
            try
            {
                var accountNumber = transactionPeriodDTO.AccountNumber;
                var startPoint = transactionPeriodDTO.StartPoint;
                var endPoint = transactionPeriodDTO.EndPoint;
                List<Transaction> transactions = transactionService.GetTransactionsForPeriod(accountNumber, startPoint, endPoint);

                return StatusCode(200, transactions);
            }
            catch(TransactionException ex) { return StatusCode(400, ex.GetErrMessage); }
        }

        [HttpGet,Route("GetCheques/{AccontNumber}"),Authorize]
        public IActionResult GetCheques(int AccountNumber)
        {
            try
            {
                List<ChequeDTO> cheques = transactionService.GetChequeDeposits(AccountNumber);

                return StatusCode(200, cheques);
            }
            catch(TransactionException ex)
            {
                return StatusCode(400, ex.ChequeErrMessage);
            }
        }
    }
}
