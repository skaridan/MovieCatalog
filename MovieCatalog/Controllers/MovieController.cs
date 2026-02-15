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
        public IActionResult All(string searchingString)
        {
            IQueryable<Movie> moviesQuery = dbContext.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchingString))
            {
                searchingString = searchingString.ToLower();

                moviesQuery = moviesQuery
                    .Where(m => m.Title.ToLower().Contains(searchingString));
            }

            IEnumerable<MovieViewModel> allMovies = moviesQuery
                .AsNoTracking()
                .Select(m => new MovieViewModel
                {
                    Id = m.Id,
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

            ViewData["CurrentFilter"] = searchingString;

            ViewData["ResultsCount"] = allMovies.Count();

            return View(allMovies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            MovieInputModel inputModel = new MovieInputModel
            {
                Genres = FetchGenres().ToList()
            };

            return View(inputModel);
        }

        [HttpPost]
        public IActionResult Create(MovieInputModel inputModel)
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
            }

            try
            {
                if (!directorExists)
                {
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

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Movie? movie = dbContext.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .AsNoTracking()
                .SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            MovieViewModel movieViewModel = new MovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                ImageUrl = movie.ImageUrl,
                ReleaseYear = movie.ReleaseYear,
                Duration = movie.Duration,
                GenreName = movie.Genre.Name,
                DirectorFirstName = movie.Director.FirstName,
                DirectorLastName = movie.Director.LastName
            };

            return View(movieViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Movie? movie = dbContext.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .AsNoTracking()
                .SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            MovieInputModel inputModel = new MovieInputModel
            {
                Title = movie.Title,
                Description = movie.Description,
                ImageUrl = movie.ImageUrl,
                ReleaseYear = movie.ReleaseYear,
                Duration = movie.Duration,
                GenreId = movie.Genre.Id,
                DirectorFirstName = movie.Director.FirstName,
                DirectorLastName = movie.Director.LastName,
                Genres = FetchGenres().ToList()
            };

            return View(inputModel);
        }

        [HttpPost]
        public IActionResult Edit(MovieInputModel inputModel, int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Movie? movie = dbContext.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

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

            try
            {
                movie.Title = inputModel.Title;
                movie.Description = inputModel.Description;
                movie.ImageUrl = inputModel.ImageUrl;
                movie.ReleaseYear = inputModel.ReleaseYear;
                movie.Duration = inputModel.Duration;
                movie.GenreId = inputModel.GenreId;
                movie.DirectorId = director.Id;

                dbContext.SaveChanges();

                return RedirectToAction(nameof(Details), new { id = movie.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                ModelState.AddModelError(string.Empty, "Unexpected error occurred while editing the movie! Please try again later.");

                return View(inputModel);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Movie? movie = dbContext.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .AsNoTracking()
                .SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            MovieDeleteViewModel viewModel = new MovieDeleteViewModel
            {
                Id = movie.Id,
                Title = movie.Title
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(MovieDeleteViewModel viewModel, int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Movie? movie = dbContext.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            try
            {
                dbContext.Movies.Remove(movie);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                ModelState.AddModelError(string.Empty, "An error occurred while deleting the movie. Please try again.");

                return RedirectToAction(nameof(Details), new { id });
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
