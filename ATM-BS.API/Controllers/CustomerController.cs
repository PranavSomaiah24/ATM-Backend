using ATM_BS.API.DTOS;
using ATM_BS.API.Entities;
using ATM_BS.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ATM_BS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IBalanceService balanceService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IBalanceService balanceService, IMapper mapper)
        {
            this.customerService = customerService; 
            this.balanceService = balanceService;
            this._mapper = mapper;

        }

        [HttpPost,Route("AddCustomer"),Authorize]
        public IActionResult Add(CustomerDTO customerDTO)
        {
            Console.WriteLine(customerDTO);
            System.Diagnostics.Debug.WriteLine(customerDTO);
            try
            {
                /* Customer customer = new Customer()
                {
                    CustomerId = customerDTO.CustomerID,
                    CustomerName = customerDTO.CustomerName,
                    AccountType = customerDTO.AccountType,
                    AccountNumber = customerDTO.AccountNumber,
                    Address = customerDTO.Address,
                    Pincode = customerDTO.Pincode,
                    Email = customerDTO.Email,
                    Contact = customerDTO.Contact,
                }; */

                Random _rdm = new Random();

                Customer customer = _mapper.Map<Customer>(customerDTO);
                customer.AccountPin = _rdm.Next(1000, 9999);
                customerService.AddCustomer(customer);

                Balance balance = new Balance()
                {
                    AccountBalance = 3000,
                    AccountNumber = customer.AccountNumber,
                };
                balanceService.AddBalance(balance);

                return StatusCode(200, customer);
                

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
                Balance balance = balanceService.GetBalance(customer.AccountNumber);
                /* CustomerAndBalanceDTO customerDTO = new CustomerAndBalanceDTO()
                 {
                     CustomerID = customer.CustomerId,
                     CustomerName = customer.CustomerName,
                     AccountType = customer.AccountType,
                     Address = customer.Address,
                     Pincode = customer.Pincode,
                     Email = customer.Email,
                     Contact = customer.Contact,
                     AccountNumber = customer.AccountNumber,
                     AccountBalance = balance.AccountBalance
                 }; */
                
                CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(customer);
                BalanceDTO balanceDTO = _mapper.Map<BalanceDTO>(balance);
                return StatusCode(200, customerDTO);
            }
            catch(Exception) { throw; }
        }

        [HttpGet, Route("DeleteCustomer/{id}"), Authorize]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                Customer customer = customerService.GetCustomer(id);
                customerService.DeleteCustomer(customer);
                return StatusCode(200);
            }
            catch (Exception) { throw; }
        }

        [HttpPut,Route("EditCustomer"),Authorize]
        public IActionResult EditCustomer(CustomerDTO customerDTO)
        {
            try
            {
                /* Customer customer = new Customer()
                {
                    CustomerId = customerDTO.CustomerID,
                    CustomerName = customerDTO.CustomerName,
                    AccountType = customerDTO.AccountType,
                    Address = customerDTO.Address,
                    Pincode = customerDTO.Pincode,
                    Email = customerDTO.Email,
                    Contact = customerDTO.Contact,
                    AccountNumber = customerDTO.AccountNumber,
                }; */
                Customer customer = _mapper.Map<Customer>(customerDTO);

                customerService.EditCustomer(customer);
                return StatusCode(200, customerDTO);
            }
            catch (Exception) { throw; }
        }

        [HttpPut,Route("ChangePin")]
        public IActionResult ChangePin(PinDTO pinDTO)
        {
            try
            {
                Customer customer = customerService.GetCustomer(pinDTO.CustomerId);
                if(customer.AccountPin != pinDTO.OldAccountPin)
                {
                    throw new Exception("Old Account Pin does not match");
                }
                customer.AccountPin = pinDTO.NewAccountPin;
                customerService.EditCustomer(customer);
                return StatusCode(200, pinDTO);
            }
            catch(Exception) { throw; }
        }
    }
}
