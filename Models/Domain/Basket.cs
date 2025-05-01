using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibProject.Models.Domain
{
    [Table("Basket")]
    public class Basket
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("book_id")]
        public int BookId { get; set; }

        [Column("reader_id")]
        public int ReaderId { get; set; }

        [Column("added_date")]
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;

        public virtual Book Book { get; set; } = null!;
        public virtual Reader Reader { get; set; } = null!;
    }
}