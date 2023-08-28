using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;

namespace ATM_BS.API.Service
{
    public interface ITransactionService
    {
        void AddTransaction(Transaction transaction);
        List<Transaction> GetTransactions(int AccountNumber);
        List<Transaction> GetTransactionsForPeriod(int AccountNumber, DateTime StartPoint, DateTime EndPoint);
        List<ChequeDTO> GetChequeDeposits(int AccountNumber);
    }
}