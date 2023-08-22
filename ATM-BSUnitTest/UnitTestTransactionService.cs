using Moq;
using ATM_BS.API.Controllers;
using ATM_BS.API.Entities;
using ATM_BS.API.Service;


namespace ATM_BSUnitTest
{
    public class UnitTestTransactionService
    {
        private readonly Mock<ITransactionService> transactionService;
        public UnitTestTransactionService()
        {
            transactionService = new Mock<ITransactionService>();

        }
    }
}
