using ATM_BS.API.Entities;
using ATM_BS.API.Service;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_BS.API.Services;
using Microsoft.EntityFrameworkCore;
using ATM_BS.API.Data;

using System.Xml.Linq;
using static System.TimeZoneInfo;

namespace ATM_BSUnitTest.UnitTesting
{
    public class UnitTestTransactionService
    {
        private DbContextOptions<ATMBSDbContext> dbContextOptions;
        private ATMBSDbContext db;
        private TransactionService? transactionService;
        public UnitTestTransactionService()
        {
            dbContextOptions = new DbContextOptionsBuilder<ATMBSDbContext>().UseSqlServer(Variables.ConnectionString).Options;
            db = new ATMBSDbContext(dbContextOptions);
        }

        [Fact]
        public void TestTransaction()
        {
            Transaction transaction = new Transaction
            {
                //TransactionId = ,
                FromAccountNumber = 12345678 ,
                ToAccountNumber = 98765432 ,
                //TransactionTime = "0001-01-01 00:00:00.0000000",
                Amount = 5000,
                FromAccountBalance = 8000,
                ToAccountBalance = 13000
            };

            transactionService = new TransactionService(db);

            transactionService.AddTransaction(transaction);
            //transaction = transactionService.GetTransactions(transaction.FromAccountNumber);
            Assert.NotNull(transaction);
        }
    }
}
