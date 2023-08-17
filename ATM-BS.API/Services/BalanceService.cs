using ATM_BS.API.Data;
using ATM_BS.API.Entities;

namespace ATM_BS.API.Service
{
    public class BalanceService : IBalanceService
    {
        private readonly ATMBSDbContext _dbcontext;

        public BalanceService(ATMBSDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void AddBalance(Balance balance)
        {
            _dbcontext.Balances.Add(balance);
            _dbcontext.SaveChanges();
        }

        public void EditBalance(Balance balance)
        {
            _dbcontext.Balances.Update(balance); //save records to the Balances table
            _dbcontext.SaveChanges();
        }

        public Balance GetBalance(int AccountNumber)
        {
            Balance balance = _dbcontext.Balances.Find(AccountNumber);
            return balance;
        }
    }
}