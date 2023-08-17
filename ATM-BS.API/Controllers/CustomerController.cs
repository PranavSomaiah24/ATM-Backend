using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;
using ATM_BS.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATM_BS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService; 
        }

        [HttpPost,Route("AddCustomer")]
        public IActionResult Add(CustomerDTO customerDTO)
        {
            Console.WriteLine(customerDTO);
            try
            {
                Customer customer = new Customer()
                {
                    CustomerId = customerDTO.CustomerID,
                    CustomerName = customerDTO.CustomerName,
                    AccountType = customerDTO.AccountType,
                    AccountNumber = customerDTO.AccountNumber,
                    Address = customerDTO.Address,
                    Pincode = customerDTO.Pincode,
                    Email = customerDTO.Email,
                    Contact = customerDTO.Contact,
                };

                customerService.AddCustomer(customer);
                return StatusCode(200, customerDTO);

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet,Route("GetCustomer/{id}"),Authorize]
        public IActionResult GetCustomer(int id)
        {
            try
            {
                Customer customer = customerService.GetCustomer(id);
                CustomerDTO customerDTO = new CustomerDTO()
                {
                    CustomerID = customer.CustomerId,
                    CustomerName = customer.CustomerName,
                    AccountType = customer.AccountType,
                    Address = customer.Address,
                    Pincode = customer.Pincode,
                    Email = customer.Email,
                    Contact = customer.Contact,
                    AccountNumber = customer.AccountNumber,
                };
                return StatusCode(200, customerDTO);
            }
            catch(Exception) { throw; }
        }

        [HttpPut,Route("EditCustomer")]
        public IActionResult EditCustomer(CustomerDTO customerDTO)
        {
            try
            {
                Customer customer = new Customer()
                {
                    CustomerId = customerDTO.CustomerID,
                    CustomerName = customerDTO.CustomerName,
                    AccountType = customerDTO.AccountType,
                    Address = customerDTO.Address,
                    Pincode = customerDTO.Pincode,
                    Email = customerDTO.Email,
                    Contact = customerDTO.Contact,
                    AccountNumber = customerDTO.AccountNumber,
                };
                customerService.EditCustomer(customer);
                return StatusCode(200, customerDTO);
            }
            catch (Exception) { throw; }
        }
    }
}
