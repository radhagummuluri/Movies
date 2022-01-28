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
1. HttpGet /v1/movies/search -> related to ApiA
2. HttpGet /v1/movies/topFive -> related to ApiB and ApiC(userId query param is optional)
3. HttpPost /v1/movies/movieRating -> related to ApiD
