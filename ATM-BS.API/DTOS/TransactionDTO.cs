using System.ComponentModel.DataAnnotations;

namespace ATM_BS.API.DTOS
{
    public class TransactionDTO
    {
        [Required(ErrorMessage = "Account Number is required")]
        [RegularExpression("[0-9]{8}", ErrorMessage = "Account Number should be 8 digits long")]
        public int AccountNumber { get; set; }
        [Required(ErrorMessage = "Account type cannot be empty")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Card Number cannot be empty")]
        public int CardNumber { get; set; }
        [Required(ErrorMessage = "Region cannot be empty")]
        public string Region { get; set; }
        [Required(ErrorMessage = "Amount cannot be empty")]
        [Range(0, int.MaxValue, ErrorMessage = "Amount must be positive")]
        public double Amount { get; set; }
    }
}
