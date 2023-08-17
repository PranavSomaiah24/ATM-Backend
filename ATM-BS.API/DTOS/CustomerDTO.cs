namespace ATM_BS.API.DTOS
{
    public class CustomerDTO
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string AccountType { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public int AccountNumber { get; set; }
    }
}
