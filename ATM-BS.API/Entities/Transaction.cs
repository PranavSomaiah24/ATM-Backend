using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM_BS.API.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //disable identity
        public int AccountNumber { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        public string Type { get; set; }
        [Required]
        public int CardNumber { get; set; }
        [Required]
        public DateTime TransactionTime { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        public string Region { get; set; }
        [Required]
        public double Amount;
    }
}