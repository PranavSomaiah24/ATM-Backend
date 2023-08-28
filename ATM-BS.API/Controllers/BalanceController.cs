using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;
using ATM_BS.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
namespace ATM_BS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService balanceService;
        private readonly ITransactionService transactionService;
        private readonly IMapper _mapper;

        class BalanceException : Exception
        {
            public BalanceException() { }
            public BalanceException(string message) : base(message) { }
            //public override string Message
            //{
            //    get { return "Failed to Initiate Minimum Balance"; }
            //}
            public string GetErrMessage
            {
                get { return "Failed to Fetch Balance"; }
            }
            public string EditErrMessage
            {
                get { return "Failed to Update Balance"; }
            }
        }

        public BalanceController(IBalanceService balanceService, ITransactionService transactionService, IMapper mapper)
        {
            //this.balanceService = balanceService;
            this.balanceService = balanceService;
            this.transactionService = transactionService;
            this._mapper = mapper;
        }

        [HttpPost,Route("AddBalance"),Authorize]
        public IActionResult AddBalance(BalanceDTO balanceDTO) { 
            try
            {
                /* Balance balance = new Balance()
                 {
                     AccountNumber = balanceDTO.AccountNumber,
                     AccountBalance = balanceDTO.AccountBalance
                 }; */

                Balance check = balanceService.GetBalance(balanceDTO.AccountNumber);
                if(check != null)
                {
                    throw new BalanceException("Balance record already exists");
                }

                Balance balance = _mapper.Map<Balance>(balanceDTO);
                balanceService.AddBalance(balance);

                return StatusCode(200, balanceDTO);
                
            }
            catch (BalanceException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet,Route("GetBalance/{accountNumber}"),Authorize]
        public IActionResult GetBalance(int accountNumber)
        {
            try
            {
                Balance balance = balanceService.GetBalance(accountNumber);
                if(balance == null)
                {
                    throw new BalanceException();
                }
                /* BalanceDTO balanceDTO = new BalanceDTO()
                {
                    AccountNumber = balance.AccountNumber,
                    AccountBalance = balance.AccountBalance
                };*/
                BalanceDTO balanceDTO = _mapper.Map<BalanceDTO>(balance);
                return StatusCode(200, balanceDTO);
            }
            catch (BalanceException ex)
            {
                return StatusCode(400, ex.GetErrMessage);
            }
        }

        [HttpPut,Route("EditBalance"),Authorize]
        public IActionResult EditBalance(BalanceDTO balanceDTO)
        {
            try
            {
                /* Balance balance = new Balance()
                {
                    AccountNumber = balanceDTO.AccountNumber,
                    AccountBalance = balanceDTO.AccountBalance
                }; */
                Balance check = balanceService.GetBalance(balanceDTO.AccountNumber);
                if(check == null)
                {
                    throw new BalanceException("Account number doesn't exist");
                }
                Balance balance = _mapper.Map<Balance>(balanceDTO);
                balanceService.EditBalance(balance);
                return StatusCode(200, balanceDTO);
            }
            catch(BalanceException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        /*
            this route is not being used for cheque-deposit
            cheque-deposit is being implemented in the transaction handler
            fromAccountNumber: null
            toAccountNumber: AccountNumber
        */
        [HttpPut,Route("ChequeDeposit"),Authorize]
        public IActionResult ChequeDeposit(DepositDTO depositDTO)
        {
            try
            {
                Balance balance = balanceService.GetBalance(depositDTO.AccountNumber);
                balance.AccountBalance += depositDTO.Amount;
                balanceService.EditBalance(balance);
                Transaction transaction = new Transaction()
                {
                    ToAccountNumber = depositDTO.AccountNumber,
                    TransactionTime = DateTime.Now,
                    Amount = depositDTO.Amount
                };

                transactionService.AddTransaction(transaction);
                return StatusCode(200, balance);
            }
            catch(Exception) { throw; }
        }
    }
}




  
