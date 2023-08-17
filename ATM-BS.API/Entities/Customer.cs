using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM_BS.API.Entities

{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //disable identity
        public int CustomerId { get; set; }

        [Required] //applies not null constraint
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Required] //applies not null constraint
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string AccountType { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Address { get; set; }

        public int Pincode { get; set; }

        [Required] //applies not null constraint
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        [ForeignKey("Balance, Transaction")]
        public int AccountNumber { get; set; }
        public Balance? Balance { get; set; }
        public Transaction? Transaction { get; set; }

    }
}
