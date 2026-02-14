using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using MovieCatalog.Models;

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
            IEnumerable<Movie> allMovies = dbContext.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .AsNoTracking()
                .ToList();

            return View(allMovies);
        }
    }
}
