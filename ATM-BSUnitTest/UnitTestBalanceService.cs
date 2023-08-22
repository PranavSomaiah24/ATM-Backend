using Moq;
using ATM_BS.API.Controllers;
using ATM_BS.API.Entities;
using ATM_BS.API.Service;

namespace ATM_BSUnitTest
{
    public class UnitTestBalanceService
    {
        private readonly Mock<IBalanceService> balanceService;
        public UnitTestBalanceService()
        {
            balanceService = new Mock<IBalanceService>();

        }

        [Fact]
        public void Test_AddBalance()
        {

        }

        [Fact]
        public void Test_EditBalance()
        {

        }

        [Fact]
        public void Test_GetBalance()
        {
            var balanceList = GetBalanceData();
            balanceService.Setup(x => x.GetBalance(12345678)).Returns(balanceList[1]);
            var result = balanceService.Object;
            var balanceResult = result.GetBalance(12345678);
            Assert.NotNull(balanceResult);
            Assert.Equal(balanceList[0].AccountNumber, balanceResult.AccountNumber);
            Assert.True(balanceList[1].AccountNumber == balanceResult.AccountNumber);
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
