using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibProject.Models.Domain
{
    [Table("BorrowedBook")]
    public class BorrowedBook
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("reader_id")]
        public int ReaderId { get; set; }

        [Column("book_id")]
        public int BookId { get; set; }

        [ForeignKey(nameof(ReaderId))]
        public virtual Reader Reader { get; set; } = null!;

        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; } = null!;

        [Column("borrow_date")]
        public DateTime BorrowDate { get; set; } = DateTime.UtcNow;

        [Column("return_date")]
        public DateTime? ReturnDate { get; set; }
    }
}