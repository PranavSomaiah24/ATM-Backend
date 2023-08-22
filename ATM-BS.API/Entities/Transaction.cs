using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM_BS.API.Entities
{
    public class Transaction
    {
        [Key]
       // public int Id { get; set; }
        public Guid TransactionId { get; set; }
        public int? FromAccountNumber { get; set; }
        public int? ToAccountNumber { get; set; }
        [Required]
        public DateTime TransactionTime { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public double FromAccountBalance { get; set; }
        [Required]
        public double ToAccountBalance { get; set; }
    }
}