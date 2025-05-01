using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibProject.Models.Domain
{
    [Table("Genre")]
    public class Genre
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("genresname")]
        [MaxLength(50)]
        [Display(Name = "Название жанра")]
        public string Name { get; set; } = string.Empty;
    }
}