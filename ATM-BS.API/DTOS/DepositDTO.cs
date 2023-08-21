using System.ComponentModel.DataAnnotations;

namespace ATM_BS.API.DTOS
{
    public class DepositDTO
    {
        [Required(ErrorMessage = "Account Number is required")]
        [RegularExpression("[0-9]{8}", ErrorMessage = "Account Number should be 12 digits long")]
        public int AccountNumber { get; set; }
        [Required(ErrorMessage = "Amount cannot be empty")]
        [Range(0, int.MaxValue, ErrorMessage = "Amount must be positive")]
        public double Amount { get; set; }
    }
}
