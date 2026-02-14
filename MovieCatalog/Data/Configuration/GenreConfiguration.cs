using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCatalog.Models;

namespace MovieCatalog.Data.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        private readonly Genre[] Genres =
        [
            new Genre
            {
                Id = 1,
                Name = "Sci-Fi"
            },
            new Genre
            {
                Id = 2,
                Name = "Drama"
            },
            new Genre
            {
                Id = 3,
                Name = "Action"
            }
        ];
        public void Configure(EntityTypeBuilder<Genre> entity)
        {
            entity.HasData(Genres);
        }
    }
}
