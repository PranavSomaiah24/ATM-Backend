using ATM_BS.API.Entities;

namespace ATM_BS.API.Service
{
    public interface IBalanceService
    {
        void AddBalance(Balance balance);
        void EditBalance(Balance balance);
        Balance GetBalance(int AccountNumber);
    }
}