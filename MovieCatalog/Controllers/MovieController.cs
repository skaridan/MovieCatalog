using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using MovieCatalog.ViewModels;

namespace MovieCatalog.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieCatalogDbContext dbContext;

        public MovieController(MovieCatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult All()
        {
            IEnumerable<MovieAllViewModel> allMovies = dbContext.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .AsNoTracking()
                .Select(m => new MovieAllViewModel
                {
                    Title = m.Title,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl,
                    ReleaseYear = m.ReleaseYear,
                    Duration = m.Duration,
                    GenreName = m.Genre.Name,
                    DirectorFirstName = m.Director.FirstName,
                    DirectorLastName = m.Director.LastName
                })
                .OrderBy(m => m.Title)
                .ThenBy(m => m.GenreName)
                .ToList();

            return View(allMovies);
        }
    }
}
