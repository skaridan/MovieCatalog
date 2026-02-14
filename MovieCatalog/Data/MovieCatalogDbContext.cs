using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MovieCatalog.Models;

namespace MovieCatalog.Data
{
    public class MovieCatalogDbContext : IdentityDbContext
    {
        public MovieCatalogDbContext(DbContextOptions<MovieCatalogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; } = null!;

        public virtual DbSet<Genre> Genres { get; set; } = null!;

        public virtual DbSet<Director> Directors { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(MovieCatalogDbContext).Assembly);
        }
    }
}
