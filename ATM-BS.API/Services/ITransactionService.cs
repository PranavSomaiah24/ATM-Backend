using ATM_BS.API.Entities;

namespace ATM_BS.API.Service
{
    public interface ITransactionService
    {
        void AddTransaction(Transaction transaction);
        List<Transaction> GetTransactions(int AccountNumber);
        //Transaction GetTransaction(long AccountNumber);
    }
}