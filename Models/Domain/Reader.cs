using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace LibProject.Models.Domain
{
    [Table("Reader")]
    public class Reader
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("first_name")]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Column("last_name")]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Column("password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        // Навигационные свойства
        public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();
        public virtual ICollection<Favorites> Favorites { get; set; } = new List<Favorites>();
        public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();
    }
}