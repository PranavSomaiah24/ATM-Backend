using ATM_BS.API.Entities;

namespace ATM_BS.API.Service
{
    public interface ICustomerService
    {
        Customer AddCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        List<Customer> GetCustomers();
        Customer GetCustomer(int CustomerId);
        void EditCustomer(Customer customer);
    }
}