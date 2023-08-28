namespace ATM_BS.API.DTOS
{
    public class ChequeDTO
    {
        public Guid ChequeId { get; set; }
        public int? AccountNumber { get; set; }
        public DateTime IssueTime { get; set; }
        public double Amount { get; set; }
        //public double AccountBalance { get; set; }
        public bool Status { get; set; }
    }
}
