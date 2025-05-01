using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibProject.Models.Domain
{
    public class Favorites
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("book_id")]
        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!; // Добавлен virtual

        [ForeignKey("reader_id")]
        public int ReaderId { get; set; }
        public virtual Reader Reader { get; set; } = null!; // Добавлен virtual
    }
}