using ATM_BS.API.Data;
using ATM_BS.API.Entities;

namespace ATM_BS.API.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ATMBSDbContext _dbcontext;

        public CustomerService(ATMBSDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Customer AddCustomer(Customer customer)
        {
            var result = _dbcontext.Customers.Add(customer);
            _dbcontext.SaveChanges();
            return result.Entity;
        }

        public void DeleteCustomer(Customer customer)
        {
            _dbcontext.Customers.Remove(customer);
            _dbcontext.SaveChanges();
        }

        public void EditCustomer(Customer customer)
        {
            _dbcontext.Customers.Update(customer);
            _dbcontext.SaveChanges();
        }

        public List<Customer> GetCustomers()
        {
            return _dbcontext.Customers.ToList();
        }

        public Customer GetCustomer(int CustomerId)
        {
            Customer customer = _dbcontext.Customers.Find(CustomerId);
            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = _dbcontext.Customers.ToList();
            return customers;
        }
    }
}