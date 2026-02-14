namespace MovieCatalog.ViewModels.Movie
{
    public class MovieAllViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public DateOnly ReleaseYear { get; set; }

        public int Duration { get; set; }

        public string GenreName { get; set; } = null!;

        public string DirectorFirstName { get; set; } = null!;

        public string DirectorLastName { get; set; } = null!;
    }
}
