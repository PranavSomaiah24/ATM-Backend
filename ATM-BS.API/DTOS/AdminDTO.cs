using System.ComponentModel.DataAnnotations;
namespace ATM_BS.API.DTOS
{
    public class AdminDTO
    {
        [Required(ErrorMessage = "Admin ID is required")]
        [RegularExpression("[0-9]{6}",ErrorMessage="Invalid Admin ID")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,16}")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Invalid EmailId")]
        public string Email { get; set; }
    }
}
