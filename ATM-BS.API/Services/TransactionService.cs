using ATM_BS.API.Data;
using ATM_BS.API.Entities;

namespace ATM_BS.API.Service
{
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
                List<Transaction> transactions = (from e in _dbcontext.Transactions
                                                  where e.AccountNumber == AccountNumber
                                                  select new Transaction()
                                                  {
                                                      AccountNumber = e.AccountNumber,
                                                      Type = e.Type,
                                                      CardNumber = e.CardNumber,
                                                      TransactionTime = e.TransactionTime,
                                                      Region = e.Region,
                                                      Amount = e.Amount,
                                                  }).ToList();
                return transactions;
            }
            catch (Exception )
            {
                throw;
            }
        }




        //public Transaction GetTransaction(long AccountNumber)
        // {
        //   Transaction transaction = _dbcontext.Transactions.Find(AccountNumber);
        //    return transaction;
        //}
    }
}