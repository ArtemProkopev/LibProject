using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibProject.Models.Domain
{
    [Table("Favorites")]
    public class Favorites
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("BookId")]
        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Column("ReaderId")]
        [ForeignKey("Reader")]
        public int ReaderId { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual Reader Reader { get; set; } = null!;
    }
}