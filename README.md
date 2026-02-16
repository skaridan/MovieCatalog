# MovieCatalog
What is the project about?
MovieCatalog is a web application for managing a personal movie collection. Users can add, view, edit, and delete movies with details like title, description, release year, duration, genre, and director.

# Main Features
Create – Add new movies with image URL, title, description, year, duration, genre, and director
Read – View all movies in a table with search by title
Update – Edit existing movie information
Delete – Remove movies with confirmation
Search – Filter movies by title (press Enter to search)
Details page – See complete movie information

# Technologies Used
ASP.NET Core 8.0
Entity Framework Core
SQL Server
Bootstrap 5
HTML5 / CSS3

# Setup Instructions
1. Clone the repository
2. Open in Visual Studio
3. Update database connection in appsettings.json:
json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MovieCatalog;Trusted_Connection=True;MultipleActiveResultSets=true"
}
4. Apply migrations
5. Run the project
6. Open in browser
