using System.ComponentModel.DataAnnotations;

namespace ATM_BS.API.DTOS
{
    public class TransactionPeriodDTO
    {
        [Required(ErrorMessage = "Account Number is required")]
        [RegularExpression("[0-9]{8}", ErrorMessage = "Account Number must be 8 digits long")]
        public int AccountNumber { get; set; }
        [Required(ErrorMessage = "Starting time is required")]
        public DateTime StartPoint { get; set; }
        [Required(ErrorMessage = "Ending time is required")]
        public DateTime EndPoint { get; set; }
    }
}
