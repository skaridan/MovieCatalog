using System.ComponentModel.DataAnnotations;

using static MovieCatalog.Common.EntityValidationConstants.Movie;

using static MovieCatalog.Common.EntityValidationConstants.Director;


namespace MovieCatalog.ViewModels.Movie
{
    public class MovieAddInputModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MinLength(ImageUrlMinLenght)]
        [MaxLength(ImageUrlMaxLenght)]
        [Url]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public DateOnly ReleaseYear { get; set; }

        [Required]
        [Range(DurationMinSpan, DurationMaxSpan)]
        public int Duration { get; set; }

        [Required]
        public int GenreId { get; set; }

        public virtual ICollection<GenreViewModel> Genres { get; set; }
            = new List<GenreViewModel>();

        [Required]
        [MinLength(DirectorFirstNameMinLength)]
        [MaxLength(DirectorFirstNameMaxLength)]
        public string DirectorFirstName { get; set; } = null!;

        [Required]
        [MinLength(DirectorLastNameMinLength)]
        [MaxLength(DirectorLastNameMaxLength)]
        public string DirectorLastName { get; set; } = null!;
    }
}
