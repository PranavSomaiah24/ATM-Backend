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
        public double Amount;
        [Required]
        public double? FromAccountBalance;
        [Required]
        public double? ToAccountBalance;
    }
}