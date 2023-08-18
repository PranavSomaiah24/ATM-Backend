using System.ComponentModel.DataAnnotations;

namespace ATM_BS.API.DTOS
{
    public class FundTransferDTO
    {

        [Required(ErrorMessage = "From account Number is required")]
        [RegularExpression("[0-9]{8}", ErrorMessage = "Account Number should be 8 digits long")]
        public int FromAccountNumber { get; set; }

        [Required(ErrorMessage = "To account Number is required")]
        [RegularExpression("[0-9]{8}", ErrorMessage = "Account Number should be 8 digits long")]
        public int ToAccountNumber { get; set;}
        [Required(ErrorMessage = "Amount to be transferred is required")]
        public double Amount { get; set; }
    }
}
