using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static MovieCatalog.Common.EntityValidationConstants.Movie;

namespace MovieCatalog.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MovieTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public DateOnly ReleaseYear { get; set; }

        public int Duration { get; set; }

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; } = null!;

        [ForeignKey(nameof(Director))]
        public int DirectorId { get; set; }

        public virtual Director Director { get; set; } = null!;
    }
}
