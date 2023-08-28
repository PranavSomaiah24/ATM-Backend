using ATM_BS.API.Data;
using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;

namespace ATM_BS.API.Service
{
    class TempBalance
    {
        public double balance { get; set; }
    }

    public class TransactionService : ITransactionService
    {
        private readonly ATMBSDbContext _dbcontext;

        public TransactionService(ATMBSDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void AddTransaction(Transaction transaction)
        {
            _dbcontext.Transactions.Add(transaction);
            _dbcontext.SaveChanges();
        }

        public List<Transaction> GetTransactions(int AccountNumber)
        {
            try
            {
                List<Transaction> transactions = (List<Transaction>)(from e in _dbcontext.Transactions
                                                  where e.FromAccountNumber == AccountNumber || e.ToAccountNumber == AccountNumber
                                                  select new Transaction()
                                                  {
                                                      ToAccountNumber = e.ToAccountNumber,
                                                      FromAccountBalance = e.FromAccountBalance,
                                                      ToAccountBalance = e.ToAccountBalance,
                                                      FromAccountNumber = e.FromAccountNumber,
                                                      TransactionTime = e.TransactionTime,
                                                      TransactionId = e.TransactionId,
                                                      Amount = e.Amount,
                                                  }).OrderBy(e => e.TransactionTime).ToList();
                return transactions;
            }
            catch (Exception )
            {
                throw;
            }
        }

        public List<Transaction> GetTransactionsForPeriod(int AccountNumber, DateTime StartPoint, DateTime EndPoint)
        {
            try
            {
                List<Transaction> transactions = (List<Transaction>)(from e in _dbcontext.Transactions
                                                                     where (e.FromAccountNumber == AccountNumber || e.ToAccountNumber == AccountNumber)
                                                                     && (e.TransactionTime >= StartPoint && e.TransactionTime <= EndPoint)
                                                                     select new Transaction()
                                                                     {
                                                                         ToAccountNumber = e.ToAccountNumber,
                                                                         FromAccountBalance = e.FromAccountBalance,
                                                                         ToAccountBalance = e.ToAccountBalance,
                                                                         FromAccountNumber = e.FromAccountNumber,
                                                                         TransactionTime = e.TransactionTime,
                                                                         TransactionId = e.TransactionId,
                                                                         Amount = e.Amount,
                                                                     }).OrderBy(e => e.TransactionTime).ToList();
                return transactions;
            }
            catch (Exception ) { throw; }
        }

        public List<ChequeDTO> GetChequeDeposits(int AccountNumber)
        {
            try
            {
                //List<ChequeDTO> cheques = (List<ChequeDTO>)(from e in _dbcontext.Transactions
                //                                            where e.FromAccountNumber == null && e.ToAccountNumber == AccountNumber
                //                                            select new ChequeDTO()
                //                                            {
                //                                                ChequeId = e.TransactionId,
                //                                                AccountNumber = e.ToAccountNumber,
                //                                                IssueTime = e.TransactionTime,
                //                                                Amount = e.Amount,
                //                                                Status = true
                //                                            });

                List<Transaction> transactions = (List<Transaction>)(from e in _dbcontext.Transactions
                                                                     where e.FromAccountNumber == null && e.ToAccountNumber == AccountNumber
                                                                     select new Transaction()
                                                                     {
                                                                         ToAccountNumber = e.ToAccountNumber,
                                                                         FromAccountBalance = e.FromAccountBalance,
                                                                         ToAccountBalance = e.ToAccountBalance,
                                                                         FromAccountNumber = e.FromAccountNumber,
                                                                         TransactionTime = e.TransactionTime,
                                                                         TransactionId = e.TransactionId,
                                                                         Amount = e.Amount,
                                                                     }).OrderBy(e => e.TransactionTime).ToList();

                //List<TempBalance> balances = (List<TempBalance>)(from e in _dbcontext.Transactions
                //                                                     where e.FromAccountNumber == AccountNumber || e.ToAccountNumber == AccountNumber
                //                                                     orderby e.TransactionTime descending
                //                                                     select new TempBalance()
                //                                                     {
                //                                                         balance = e.ToAccountBalance
                //                                                     }).ToList();

                //double balance = balances[0].balance;

                List<ChequeDTO> cheques = new List<ChequeDTO>() { };

                foreach(var t in transactions)
                {
                    cheques.Add(new ChequeDTO()
                    {
                        ChequeId = t.TransactionId,
                        AccountNumber = t.ToAccountNumber,
                        IssueTime = t.TransactionTime,
                        Amount = t.Amount,
                        Status = true
                    });
                }

                // ADDING DUMMY DATA FOR PENDING CHEQUES
                DateTime dummy = DateTime.Now;

                cheques.Add(new ChequeDTO()
                {
                    ChequeId = Guid.NewGuid(),
                    AccountNumber = AccountNumber,
                    IssueTime = dummy.AddHours(-2),
                    Amount = 2000,
                    Status = false
                });

                cheques.Add(new ChequeDTO()
                {
                    ChequeId = Guid.NewGuid(),
                    AccountNumber = AccountNumber,
                    IssueTime = dummy.AddHours(-1),
                    Amount = 4000,
                    Status = false
                });

                return cheques;
            }
            catch (Exception ) { throw; }
        }
    }
}