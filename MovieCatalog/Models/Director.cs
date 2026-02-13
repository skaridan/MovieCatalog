using System.ComponentModel.DataAnnotations;

using static MovieCatalog.Common.EntityValidationConstants.Director;

namespace MovieCatalog.Models
{
    public class Director
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DirectorFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(DirectorLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public DateTime? BirthDate { get; set; }

        public ICollection<Movie> Movies { get; set; } 
            = new List<Movie>();
    }
}
