namespace ATM_BS.API.DTOS
{
    public class TransactionDisplayDTO
    {
        public Guid TransactionId { get; set; }
        public int? FromAccountNumber { get; set; }
        public int? ToAccountNumber { get; set; }
        public DateTime TransactionTime { get; set; }
        public double Amount { get; set; }
        public double? FromAccountBalance { get; set; }
        public double? ToAccountBalance { get; set; }
    }
}
