using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM_BS.API.Entities

{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //disable identity
        public int Id { get; set; }

        [Required] //applies not null constraint
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required] //applies not null constraint
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Email { get; set; }

        public long Contact { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Password { get; set; }

    }
}
