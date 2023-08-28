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

        class CustomerException : Exception
        {
            public CustomerException()
            {
                
            }
            public CustomerException(string message) : base(message)
            {
                
            }
            public override string Message
            {
                get { return "Failed to Register Customer"; }
            }
            public string GetErrMessage
            {
                get { return "Failed to Fetch Customer"; }
            }
            public string DeleteErrMessage
            {
                get { return "Failed to Delete Customer"; }
            }
            public string EditErrMessage
            {
                get { return "Failed to Edit Customer"; }
            }
            public string PinErrMessage
            {
                get { return "Failed to Change Pin"; }
            }
        }

        [HttpPost,Route("AddCustomer"),Authorize]
        public IActionResult Add(CustomerDTO customerDTO)
        {
            Console.WriteLine(customerDTO);
            System.Diagnostics.Debug.WriteLine(customerDTO);
            try
            {
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
            catch (CustomerException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet,Route("GetCustomer/{id}"),Authorize]
        public IActionResult GetCustomer(int id)
        {
            try
            {
                Customer customer = customerService.GetCustomer(id);
                Balance balance = balanceService.GetBalance(customer.AccountNumber);
                
                CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(customer);
                BalanceDTO balanceDTO = _mapper.Map<BalanceDTO>(balance);
                return StatusCode(200, customerDTO);
            }
            catch(CustomerException ex)
            {
                return StatusCode(400, ex.GetErrMessage);
            }
        }

        [HttpDelete,Route("DeleteCustomer/{id}"),Authorize]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                Customer customer = customerService.GetCustomer(id);
                customerService.DeleteCustomer(customer);
                return StatusCode(200);
            }
            catch (CustomerException ex)
            {
                return StatusCode(400, ex.DeleteErrMessage);
            }
        }

        [HttpPut,Route("EditCustomer"),Authorize]
        public IActionResult EditCustomer(CustomerDTO customerDTO)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(customerDTO);

                customerService.EditCustomer(customer);
                return StatusCode(200, customerDTO);
            }
            catch (CustomerException ex)
            {
                return StatusCode(400, ex.EditErrMessage);
            }
        }

        [HttpPut,Route("ChangePin")]
        public IActionResult ChangePin(PinDTO pinDTO)
        {
            try
            {
                Customer customer = customerService.GetCustomer(pinDTO.CustomerId);
                if(customer.AccountPin != pinDTO.OldAccountPin)
                {
                    throw new CustomerException("Old Account Pin does not match");
                }
                customer.AccountPin = pinDTO.NewAccountPin;
                customerService.EditCustomer(customer);
                return StatusCode(200, pinDTO);
            }
            catch(CustomerException ex)
            {
                return StatusCode(400, ex.PinErrMessage);
            }
        }
    }
}
