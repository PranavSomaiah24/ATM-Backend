using Moq;
using ATM_BS.API.Controllers;
using ATM_BS.API.Entities;
using ATM_BS.API.Service;
using ATM_BS.API.Services;
using ATM_BS.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ATM_BSUnitTest.UnitTesting
{
    public class UnitTestBalanceService
    {
        private DbContextOptions<ATMBSDbContext> dbContextOptions;
        private ATMBSDbContext db;
        private BalanceService? balanceService;

        public UnitTestBalanceService()
        {
            dbContextOptions = new DbContextOptionsBuilder<ATMBSDbContext>().UseSqlServer("Data Source=WINDOWS-BVQNF6J;Initial Catalog=bank;Persist Security Info=True;User ID=sa;Password=12345;TrustServerCertificate=True").Options;
            //dbContextOptions = new DbContextOptionsBuilder<ATMBSDbContext>().UseInMemoryDatabase(dbName).Options;
            db = new ATMBSDbContext(dbContextOptions);
        }

        [Fact]
        public void Test_AddBalance()
        {
            var balance = new Balance
            {

                AccountNumber = 11122233,
                AccountBalance = 3000
            };
            balanceService = new BalanceService(db);
            balance = balanceService.AddBalance(balance);
            Assert.NotNull(balance);

            balance = balanceService.Validate(balance.AccountNumber, balance.AccountBalance);
            balance = balanceService.Validate(balance.AccountNumber, balance.AccountBalance);
            Assert.NotNull(balance);

        }

        [Fact]
        public void Test_EditBalance()
        {

        }

        [Fact]
        public Balance Test_GetBalance()
        {
            var balanceList = GetBalanceData();
            balanceService.Setup(x => x.GetBalance(12345678)).Returns(balanceList[1]);
            var result = balanceService.Object;
            var balanceResult = result.GetBalance(12345678);
            Assert.NotNull(balanceResult);
            Assert.Equal(balanceList[0].AccountNumber, balanceResult.AccountNumber);
            Assert.True(balanceList[1].AccountNumber == balanceResult.AccountNumber);
            return balanceResult;
        }

        private List<Balance> GetBalanceData()
        {
            List<Balance> balanceData = new List<Balance>
            {
                new Balance
                {
                    AccountNumber = 12345678,
                    AccountBalance = 3000
                },
                new Balance
                {
                    AccountNumber = 12345679,
                    AccountBalance = 4000
                },
                new Balance
                {
                    AccountNumber = 65473621,
                    AccountBalance = 0
                }

            };
            return balanceData;
        }
    }
}
