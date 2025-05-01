using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace LibProject.Models.Domain
{
    [Table("Books")]
    public class Book
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("title")]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column("author")]
        [MaxLength(100)]
        public string Author { get; set; } = string.Empty;

        [Column("year")]
        public int Year { get; set; }

        [Column("price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Column("image_url")]
        [Url]
        public string ImageUrl { get; set; } = string.Empty;

        [Column("is_available")]
        public bool IsAvailable { get; set; } = true;

        [Column("genre_id")]
        [ForeignKey("Genre")]
        public int? GenreId { get; set; }
        public virtual Genre Genre { get; set; } = null!;

        // Навигационные свойства
        public virtual ICollection<Favorites> Favorites { get; set; } = new List<Favorites>();
        public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();
        public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();

        [NotMapped]
        public bool IsFavorite { get; set; }
    }
}