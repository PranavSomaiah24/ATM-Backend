namespace ATM_BS.API.DTOS
{
    public class TransactionDTO
    {
        public int AccountNumber { get; set; }
        public string Type { get; set; }
        public int CardNumber { get; set; }
        public string Region { get; set; }
        public double Amount { get; set; }
    }
}
