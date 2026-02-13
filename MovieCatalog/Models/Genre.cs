using System.ComponentModel.DataAnnotations;

using static MovieCatalog.Common.EntityValidationConstants.Genre;

namespace MovieCatalog.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GenreNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; } 
            = new HashSet<Movie>();
    }
}
