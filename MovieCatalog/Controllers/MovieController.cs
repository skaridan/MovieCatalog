using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.Data;
using MovieCatalog.Models;
using MovieCatalog.ViewModels.Movie;

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

        [HttpGet]
        public IActionResult Create()
        {
            MovieAddInputModel inputModel = new MovieAddInputModel
            {
                Genres = FetchGenres().ToList()
            };

            return View(inputModel);
        }

        [HttpPost]
        public IActionResult Create(MovieAddInputModel inputModel)
        {
            inputModel.Genres = FetchGenres().ToList();

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            if (!GenreExists(inputModel.GenreId))
            {
                ModelState.AddModelError(nameof(inputModel.GenreId), "Invalid Genre is selected!");

                return View(inputModel);
            }

            try
            {
                bool directorExists =
                    DirectorExists(inputModel.DirectorFirstName, inputModel.DirectorLastName);

                Director director;

                if (directorExists)
                {
                    director = dbContext.Directors
                        .FirstOrDefault(d => d.FirstName == inputModel.DirectorFirstName
                        && d.LastName == inputModel.DirectorLastName)!;
                }
                else
                {
                    director = new Director
                    {
                        FirstName = inputModel.DirectorFirstName,
                        LastName = inputModel.DirectorLastName,
                        BirthDate = DateTime.UtcNow
                    };

                    dbContext.Directors.Add(director);
                    dbContext.SaveChanges();
                }

                Movie movie = new Movie
                {
                    Title = inputModel.Title,
                    Description = inputModel.Description,
                    ImageUrl = inputModel.ImageUrl,
                    ReleaseYear = inputModel.ReleaseYear,
                    Duration = inputModel.Duration,
                    GenreId = inputModel.GenreId,
                    DirectorId = director.Id
                };

                dbContext.Movies.Add(movie);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                ModelState.AddModelError(string.Empty, "Unexpected error occurred while adding the movie! Please try again later.");

                return View(inputModel);
            }
        }

        private IEnumerable<GenreViewModel> FetchGenres()
        {
            return dbContext.Genres
                .AsNoTracking()
                .Select(g => new GenreViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                })
                .OrderBy(g => g.Name)
                .ToList();
        }

        private bool GenreExists(int genreId)
        {
            return dbContext.Genres.Any(g => g.Id == genreId);
        }

        private bool DirectorExists(string firstName, string lastName)
        {
            return dbContext.Directors
                .Any(d => d.FirstName == firstName && d.LastName == lastName);
        }
    }
}
