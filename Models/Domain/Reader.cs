using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibProject.Models
{
    public class Reader
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("firstname")]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Column("lastname")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [Column("password")]
        [Required]
        public string Password { get; set; }
    }
}
