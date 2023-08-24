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
    public class UnitTestBalanceService
    {
        private DbContextOptions<ATMBSDbContext> dbContextOptions;
        private ATMBSDbContext db;
        private BalanceService? balanceService;
        public UnitTestBalanceService()
        {
            dbContextOptions = new DbContextOptionsBuilder<ATMBSDbContext>().UseSqlServer(Variables.ConnectionString).Options;
            db = new ATMBSDbContext(dbContextOptions);
        }

        [Fact]
        public void TestBalance()
        {
            Balance balance = new Balance
            {
                AccountNumber = 66666666,
                AccountBalance = 13000
            };

            balanceService = new BalanceService(db);

            balanceService.AddBalance(balance);
            balance = balanceService.GetBalance(balance.AccountNumber);
            balanceService.EditBalance(balance);
            Assert.NotNull(balance);

            db.Balances.Remove(balance);
            db.SaveChanges();
        }
    }
}
