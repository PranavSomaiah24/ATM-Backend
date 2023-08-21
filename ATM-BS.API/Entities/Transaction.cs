using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM_BS.API.Entities
{
    public class Transaction
    {
        [Key]
       // public int Id { get; set; }
        public int TId { get; set; }
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Type { get; set; }
        [Required]
        public int CardNumber { get; set; }
        [Required]
        public DateTime TransactionTime { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Region { get; set; }
        [Required]
        public double Amount;
    }
}