using System.ComponentModel.DataAnnotations;
namespace ATM_BS.API.DTOS
{
    public class AdminDTO
    {
        [Required(ErrorMessage = "Admin ID is required")]
        [RegularExpression("[0-9]{6}",ErrorMessage="Invalid Admin ID")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Admin Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,16}",ErrorMessage ="Password must contain an uppercase alphabet, a lower case alphabet, a number, a special character and the length should be in between 8 and 16(inclusive)")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$",ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}
