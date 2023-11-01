# Movies
Movies database application

Name of database: Movies
Tables in the database
- dbo.Movies
- dbo.Users
- dbo.Genres
- dbo.MovieGenres
- dbo.MovieRatings

Run database migration 
```
dotnet ef database update
```

Data is seeded in the database when the application runs for the first time.

Swagger url when application is run from visual studio - http://localhost:5000/swagger/index.html

List of Apis
1. HttpGet /v1/movies/search
2. HttpGet /v1/movies/topFive
3. HttpPost /v1/movies/movieRating
