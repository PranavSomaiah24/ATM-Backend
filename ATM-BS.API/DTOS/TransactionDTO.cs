using System.ComponentModel.DataAnnotations;

namespace ATM_BS.API.DTOS
{
    public class TransactionDTO
    {
        [RegularExpression("[0-9]{8}", ErrorMessage = "Account Number should be 8 digits long")]
        public int FromAccountNumber { get; set; }
        [RegularExpression("[0-9]{8}", ErrorMessage = "Account Number should be 8 digits long")]
        public int ToAccountNumber { get; set; }
        [Required(ErrorMessage = "Amount cannot be empty")]
        [Range(0, int.MaxValue, ErrorMessage = "Amount must be positive")]
        public double Amount { get; set; }
    }
}
