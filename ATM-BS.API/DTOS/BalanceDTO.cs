using System.ComponentModel.DataAnnotations;

namespace ATM_BS.API.DTOS
{
    public class BalanceDTO
    {
        [Required(ErrorMessage = "Please enter Account Number")]
        [RegularExpression("[0-9]{8}",ErrorMessage = "Account Number should be 8 digits long")]
        public int AccountNumber { get; set; }
        [Required(ErrorMessage = "Please enter Account Balance")]
        public double AccountBalance { get; set; }
    }
}
