using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCatalog.Models;

namespace MovieCatalog.Data.Configuration
{
    public class DirectorConfiguration : IEntityTypeConfiguration<Director>
    {
        private readonly Director[] Directors =
        [
            new Director
            {
                Id = 1,
                FirstName = "Christopher",
                LastName = "Nolan",
                BirthDate = new DateTime(1970, 7, 30)
            },
            new Director
            {
                Id = 2,
                FirstName = "Frank",
                LastName = "Darabont",
                BirthDate = new DateTime(1959, 1, 28)
            },
            new Director
            {
                Id = 3,
                FirstName = "Lana",
                LastName = "Wachowski",
                BirthDate = new DateTime(1965, 6, 21)
            }
        ];
        public void Configure(EntityTypeBuilder<Director> entity)
        {
            entity.HasData(Directors);
        }
    }
}
