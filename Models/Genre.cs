using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibProject.Models
{
    public class Genre
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("genresname")]
        [MaxLength(50)]
        public string GenresName { get; set; }

    }
}
