using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibProject.Models
{
    [Table("Book")]
    public class Book
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Column("author")]
        [MaxLength(100)]
        public string Author { get; set; }

        [Column("year")]
        public int Year { get; set; }

        [Column("isavailable")]
        public bool IsAvailable { get; set; } = true;
    }

}
