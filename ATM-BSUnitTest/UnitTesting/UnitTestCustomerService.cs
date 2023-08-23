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

namespace ATM_BSUnitTest.UnitTesting
{
    public class UnitTestCustomerService
    {
        private DbContextOptions<ATMBSDbContext> dbContextOptions;
        private ATMBSDbContext db;
        private CustomerService? customerService;
        public UnitTestCustomerService()
        {
            dbContextOptions = new DbContextOptionsBuilder<ATMBSDbContext>().UseSqlServer(Variables.ConnectionString).Options;

            // dbContextOptions = new DbContextOptionsBuilder<ATMBSDbContext>().UseSqlServer("Data Source=WINDOWS-BVQNF6J;Initial Catalog=bank;Persist Security Info=True;User ID=sa;Password=12345;TrustServerCertificate=True").Options;
            //dbContextOptions = new DbContextOptionsBuilder<ATMBSDbContext>().UseInMemoryDatabase(dbName).Options;
            db = new ATMBSDbContext(dbContextOptions);
        }

        [Fact]
        public void TestCustomer()
        {
            Customer customer = new Customer
            {
                CustomerId = 101010,
                CustomerName = "Ajay",
                AccountType = "Savings",
                Address = "Hyderabad",
                Pincode = 222222,
                Email = "ajay@gmail.com",
                Contact = "8888888888",
                AccountNumber = 45454545,
                AccountPin = 4444,
            };

            customerService = new CustomerService(db);

            customerService.AddCustomer(customer);
            customer = customerService.GetCustomer(customer.CustomerId);
            Assert.NotNull(customer);
            
            customerService.DeleteCustomer(customer);
            customer = customerService.GetCustomer(customer.CustomerId);
            Assert.Null(customer);

        }
        
        /*
        private List<Customer> GetCustomersData()
        {
            List<Customer> customersData = new List<Customer>
        {
            new Customer
            {
                CustomerId = 123456,
                CustomerName = "Ashok",
                AccountType = "Savings",
                Address = "Banglore",
                Pincode = 777777,
                Email = "ashok@gmail.com",
                Contact = "9999999999",
                AccountNumber = 12345678,
                AccountPin = 3333
    },
            new Customer
            {
                CustomerId = 654321,
                CustomerName = "Dhruva",
                AccountType = "Salary",
                Address = "Banglore",
                Pincode = 666666,
                Email = "dhruva@gmail.com",
                Contact = "9999999999",
                AccountNumber = 87654321,
                AccountPin = 4444
            },
            
        };
            return customersData;
        }
        */
    }
}
