using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ATM_BS.API.DTOS
{
    public class PinDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //disable identity
        public int CustomerId { get; set; }
        [Required]
        [RegularExpression("[1-9][0-9]{3}",ErrorMessage = "Pin must be 4-digit numeric")]
        public int OldAccountPin { get; set; }
        [Required]
        [RegularExpression("[1-9][0-9]{3}", ErrorMessage = "Pin must be 4-digit numeric")]
        public int NewAccountPin { get; set; }
    }
}
