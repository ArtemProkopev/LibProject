using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibProject.Models
{
    [Table("BookByReader")]
    public class BookByReader
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("reader_id")]
        public virtual Reader Reader { get; set; }

        [ForeignKey("book_id")]
        public virtual Book Book { get; set; }

    }
}
