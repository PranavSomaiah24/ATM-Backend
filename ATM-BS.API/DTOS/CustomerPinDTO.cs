using System.ComponentModel.DataAnnotations;

namespace ATM_BS.API.DTOS
{
    public class CustomerPinDTO
    {
        [Required(ErrorMessage = "Customer ID is required")]
        [RegularExpression("[1-9][0-9]{7}", ErrorMessage = "Customer ID should be 8 digits long")]
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Customer Name is required")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Account Type is required")]
        public string AccountType { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Pincode is required")]
        [RegularExpression("[1-9][0-9]{5}", ErrorMessage = "Pincode should be 6 digits long")]
        public int Pincode { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contact is required")]
        [RegularExpression("[6-9][0-9]{9}", ErrorMessage = "Phone number must be in Indian format")]
        public string Contact { get; set; }
        [Required(ErrorMessage = "EAccount Number is required")]
        [RegularExpression("[0-9]{8}", ErrorMessage = "Account Number should be 8 digits long")]
        public int AccountNumber { get; set; }


        [Required]
        public int AccountPin { get; set; } 
    }
}
