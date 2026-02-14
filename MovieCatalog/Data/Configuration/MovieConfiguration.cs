using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCatalog.Models;

namespace MovieCatalog.Data.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        private readonly Movie[] Movies =
        [
            new Movie
            {
                Id = 1,
                Title = "Inception",
                Description = "A skilled thief enters dreams to steal secrets but gets a chance at redemption.",
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-ptZQHmTbAZOSGg4iS64lAqsD6khvXDQX2Ocfv_bnSyNlztsrFVCbWq6kQsK5tI9myBAHtjqXvzJefVEnlxb-iRfK001cxulOVEFttnvd&s=10",
                ReleaseYear = new DateOnly(2010, 1, 1),
                Duration = 148,
                GenreId = 1,
                DirectorId = 1,
            },
            new Movie
            {
                Id = 2,
                Title = "The Shawshank Redemption",
                Description = "Two imprisoned men bond over the years and find hope through friendship.",
                ImageUrl = "https://m.media-amazon.com/images/I/51NiGlapXlL._AC_.jpg",
                ReleaseYear = new DateOnly(1994, 1, 1),
                Duration = 142,
                GenreId = 2,
                DirectorId = 2,
            },
            new Movie
            {
                Id = 3,
                Title = "The Matrix",
                Description = "A hacker discovers the truth about reality and his role in the war against its controllers.",
                ImageUrl = "https://m.media-amazon.com/images/I/51EG732BV3L.jpg",
                ReleaseYear = new DateOnly(1999, 1, 1),
                Duration = 136,
                GenreId = 1,
                DirectorId = 3,
            }
        ];

        public void Configure(EntityTypeBuilder<Movie> entity)
        {
            entity.HasData(Movies);
        }
    }
}
