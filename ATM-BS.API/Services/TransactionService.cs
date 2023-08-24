﻿using ATM_BS.API.Data;
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


        //public Transaction GetTransaction(long AccountNumber)
        // {
        //   Transaction transaction = _dbcontext.Transactions.Find(AccountNumber);
        //    return transaction;
        //}
    }
}