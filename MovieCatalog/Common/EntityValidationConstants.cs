namespace MovieCatalog.Common
{
    public static class EntityValidationConstants
    {
        public static class Movie
        {
            public const int MovieTitleMinLength = 2;
            public const int MovieTitleMaxLength = 150;

            public const int DescriptionMinLength = 50;
            public const int DescriptionMaxLength = 500;

            public const int ImageUrlMinLenght = 7;
            public const int ImageUrlMaxLenght = 2048;

            public const int DurationMinSpan = 30;
            public const int DurationMaxSpan = 240;
        }

        public static class Genre
        {
            public const int GenreNameMinLength = 3;
            public const int GenreNameMaxLength = 50;
        }

        public static class Director
        {
            public const int DirectorFirstNameMinLength = 3;
            public const int DirectorFirstNameMaxLength = 100;

            public const int DirectorLastNameMinLength = 2;
            public const int DirectorLastNameMaxLength = 50;
        }
    }
}
