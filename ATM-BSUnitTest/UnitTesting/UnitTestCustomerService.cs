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

namespace ATM_BSUnitTest.UnitTesting
{
    public class UnitTestCustomerService
    {
        private readonly Mock<ICustomerService> customerService;
        public UnitTestCustomerService()
        {
            customerService = new Mock<ICustomerService>();
        }

        [Fact]
        public void Test_AddCustomer()
        {

        }
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
                AccountNumber = 87654321
                AccountPin = 4444
            },
            
        };
            return customersData;
        }
    }
}
