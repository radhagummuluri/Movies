using Movies.RestApi.Data.Models;
using System.Linq;

namespace Movies.RestApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(MoviesContext context)
        {
            context.Database.EnsureCreated();

            if(context.Movies.Any())
            {
                return;
            }

            var movies = new Movie[]
            {
                new() { Title = "The Matrix", YearOfRelease = 1999, RunningTime = 136 },
                new() { Title = "The Matrix Resurrections", YearOfRelease = 2021, RunningTime = 148 },
                new() { Title = "Sadak 2", YearOfRelease = 2020, RunningTime = 133 },
                new() { Title = "Groundhog Day", YearOfRelease = 1993, RunningTime = 101 },
                new() { Title = "Don't Look Up", YearOfRelease = 2021, RunningTime = 143 },
                new() { Title = "Melancholia", YearOfRelease = 2011, RunningTime = 135 },
                new() { Title = "Parasite", YearOfRelease = 2019, RunningTime = 133 },
                new() { Title = "Spirited Away", YearOfRelease = 2001, RunningTime = 125 },
                new() { Title = "The Girl with the Dragon Tattoo", YearOfRelease = 2009, RunningTime = 155 }
            };

            foreach (var movie in movies)
            {
                context.Movies.Add(movie);
            }

            context.Movies.OrderBy(x => x.Title);
            context.SaveChanges();
            
            var genres = new Genre[]
            {
                new() { Name = "Action" },
                new() { Name = "Science Fiction" },
                new() { Name = "Drama" },
                new() { Name = "Bollywood" },
                new() { Name = "Romance" },
                new() { Name = "Fantasy" },
                new() { Name = "Comedy" },
                new() { Name = "Thriller" },
                new() { Name = "Korean" },
                new() { Name = "Animation" },
                new() { Name = "Crime" },
                new() { Name = "Mystery" },
                new() { Name = "Swedish" },
                new() { Name = "Adventure" },
                new() { Name = "Family" }
            };
            
            foreach (var genre in genres)
            {
                context.Genres.Add(genre);
            }

            context.Genres.OrderBy(g => g.Name);
            context.SaveChanges();

            var movieGenres = new MovieGenre[]
            {
                new() { MovieID = 1, GenreID = 9 },
                new() { MovieID = 1, GenreID = 8 },
                new() { MovieID = 2, GenreID = 8 },
                new() { MovieID = 2, GenreID = 9 },
                new() { MovieID = 2, GenreID = 14 },
                new() { MovieID = 3, GenreID = 7 },
                new() { MovieID = 3, GenreID = 6 },
                new() { MovieID = 4, GenreID = 5 },
                new() { MovieID = 4, GenreID = 7 },
                new() { MovieID = 4, GenreID = 3 },
                new() { MovieID = 5, GenreID = 3 },
                new() { MovieID = 5, GenreID = 7 },
                new() { MovieID = 5, GenreID = 8 },
                new() { MovieID = 6, GenreID = 7 },
                new() { MovieID = 6, GenreID = 8 },
                new() { MovieID = 7, GenreID = 3 },
                new() { MovieID = 7, GenreID = 2 },
                new() { MovieID = 7, GenreID = 7 },
                new() { MovieID = 7, GenreID = 1 },
                new() { MovieID = 8, GenreID = 10 },
                new() { MovieID = 8, GenreID = 15 },
                new() { MovieID = 8, GenreID = 4 },
                new() { MovieID = 9, GenreID = 7 },
                new() { MovieID = 9, GenreID = 2 },
                new() { MovieID = 9, GenreID = 11 },
                new() { MovieID = 9, GenreID = 12 },
                new() { MovieID = 9, GenreID = 13 }
            };

            foreach (var movieGenre in movieGenres)
            {
                context.MovieGenres.Add(movieGenre);
            }

            context.SaveChanges();

            var users = new User[]
            {
                new() { Name = "Mark Kermode" },
                new() { Name = "Erica Abeel" },
                new() { Name = "Roger Ebert" },                
                new() { Name = "Radha Gumm" }
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.Users.OrderBy(u => u.Name);
            context.SaveChanges();

            var ratings = new MovieRating[]
            {
                new () { UserID = 1, MovieID = 1, Rating = 5 },
                new () { UserID = 1, MovieID = 2, Rating = 3 },
                new () { UserID = 1, MovieID = 3, Rating = 1 },
                new () { UserID = 1, MovieID = 4, Rating = 4 },
                new () { UserID = 1, MovieID = 5, Rating = 5 },
                new () { UserID = 1, MovieID = 6, Rating = 5 },
                new () { UserID = 1, MovieID = 7, Rating = 4 },
                new () { UserID = 1, MovieID = 8, Rating = 5 },
                new () { UserID = 2, MovieID = 1, Rating = 4 },
                new () { UserID = 2, MovieID = 2, Rating = 2 },
                new () { UserID = 2, MovieID = 3, Rating = 1 },
                new () { UserID = 2, MovieID = 4, Rating = 3 },
                new () { UserID = 2, MovieID = 5, Rating = 4 },
                new () { UserID = 2, MovieID = 6, Rating = 5 },
                new () { UserID = 2, MovieID = 7, Rating = 4 },
                new () { UserID = 2, MovieID = 8, Rating = 5 },
                new () { UserID = 3, MovieID = 1, Rating = 5 },
                new () { UserID = 3, MovieID = 2, Rating = 2 },
                new () { UserID = 3, MovieID = 3, Rating = 1 },
                new () { UserID = 3, MovieID = 4, Rating = 2 },
                new () { UserID = 3, MovieID = 5, Rating = 3 },
                new () { UserID = 3, MovieID = 6, Rating = 3 },
                new () { UserID = 3, MovieID = 7, Rating = 4 },
                new () { UserID = 3, MovieID = 8, Rating = 4 },
                new () { UserID = 4, MovieID = 1, Rating = 5 },
                new () { UserID = 4, MovieID = 2, Rating = 3 },
                new () { UserID = 4, MovieID = 3, Rating = 1 },
                new () { UserID = 4, MovieID = 4, Rating = 4 },
                new () { UserID = 4, MovieID = 5, Rating = 4 },
                new () { UserID = 4, MovieID = 6, Rating = 4 },
                new () { UserID = 4, MovieID = 7, Rating = 4 },
                new () { UserID = 4, MovieID = 8, Rating = 5 }
            };
            
            foreach (var rating in ratings)
            {
                context.MovieRatings.Add(rating);
            }

            context.SaveChanges();
        }
    }
}
