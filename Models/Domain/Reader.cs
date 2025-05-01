using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibProject.Models.Domain
{
    [Table("Reader")]
    public class Reader
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Column("last_name")]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Column("password")]
        public string Password { get; set; } = null!;

        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        [Column("role")]
        [MaxLength(20)]
        public string Role { get; set; } = "User";

        // Навигационные свойства
        public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();
        public virtual ICollection<Favorites> Favorites { get; set; } = new List<Favorites>();
        public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();
    }
}