using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.RestApi.Data.Models;
using Movies.RestApi.Data.ReadModel;

namespace Movies.RestApi.Data.Repositories
{
    public interface IMovieRepository
    {
        bool IsValidMovie(int movieId);
        bool IsValidUser(int userId);
        Task<ICollection<MovieReadModel>> SearchMovies(string title, int yearOfRelease, HashSet<string> genres);
        Task<ICollection<MovieReadModel>> TopFiveMovies(int? userId);
        Task UpsertMovieRating(int userId, int movieId, int rating);
    }

    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesContext _moviesContext;

        public MovieRepository(MoviesContext moviesContext)
        {
            _moviesContext = moviesContext;
        }

        public bool IsValidMovie(int movieId)
        {
            return _moviesContext.Movies.Find(movieId) != null;
        }

        public bool IsValidUser(int userId)
        {
            return _moviesContext.Users.Find(userId) != null;
        }

        public async Task<ICollection<MovieReadModel>> SearchMovies(string title, int yearOfRelease, HashSet<string> genres)
        {
            var movies = (await _moviesContext.Movies
                .Include(x => x.MovieRatings)
                .Include(x => x.MovieGenres)
                .ThenInclude(y => y.Genre)
                .Where(x => string.IsNullOrEmpty(title) || (!string.IsNullOrEmpty(title) && x.Title.Contains(title)))
                .Where(x => yearOfRelease == 0 || x.YearOfRelease == yearOfRelease)
                .Select(x => new MovieReadModel
                {
                    ID = x.ID,
                    Title = x.Title,
                    YearOfRelease = x.YearOfRelease,
                    RunningTime = x.RunningTime,
                    Genres = string.Join(",", x.MovieGenres.Select(c => c.Genre.Name).ToList()),
                    CalculatedAverageRating = x.MovieRatings.Count > 0 ? x.MovieRatings.Average(y => y.Rating * 1.0) : 0
                })
                .Select(x => x)
                .AsNoTracking()
                .ToListAsync())
                .Where(movie => genres.Count == 0 || (genres.Count > 0 && genres.All(val =>
                    movie.Genres.Split(",").ToList().Contains(val, StringComparer.OrdinalIgnoreCase))))
                .ToList();
            return movies;
        }

        public async Task<ICollection<MovieReadModel>> TopFiveMovies(int? userId)
        {
            ICollection<MovieReadModel> movies = null;
            if (!userId.HasValue)
            {
                movies = await _moviesContext.Movies
                    .Include(x => x.MovieGenres)
                    .Include(x => x.MovieRatings)
                    .Select(x => new MovieReadModel
                    {
                        ID = x.ID,
                        Title = x.Title,
                        YearOfRelease = x.YearOfRelease,
                        RunningTime = x.RunningTime,
                        Genres = string.Join(",",x.MovieGenres.Select(mg => mg.Genre.Name)),
                        CalculatedAverageRating = x.MovieRatings.Average(y => y.Rating * 1.0)
                    })
                    .OrderByDescending(x => x.CalculatedAverageRating)
                    .ThenBy(x => x.Title)
                    .Take(5)
                    .AsNoTracking()
                    .ToListAsync();

                return movies;
            }

            movies = await _moviesContext.MovieRatings
                .Include(x => x.Movie.MovieGenres)
                .Where(x => x.UserID == userId.Value)
                .Select(x => new MovieReadModel
                {
                    ID = x.Movie.ID,
                    Title = x.Movie.Title,
                    YearOfRelease = x.Movie.YearOfRelease,
                    RunningTime = x.Movie.RunningTime,
                    Genres = string.Join(",", x.Movie.MovieGenres.Select(mg => mg.Genre.Name)),
                    CalculatedAverageRating = x.Rating
                })
                .OrderByDescending(x => x.CalculatedAverageRating)
                .ThenBy(x => x.Title)
                .Take(5)
                .AsNoTracking()
                .ToListAsync();

            return movies;
        }

        public async Task UpsertMovieRating(int userId, int movieId, int rating)
        {
            var movieRating = new MovieRating
            {
                MovieID = movieId, 
                UserID = userId, 
                Rating = rating
            };
            _ = _moviesContext.MovieRatings.Any(mr => mr.UserID == movieRating.UserID && mr.MovieID == movieRating.MovieID)
                ? _moviesContext.MovieRatings.Update(movieRating)
                : _moviesContext.MovieRatings.Add(movieRating);
            
            await _moviesContext.SaveChangesAsync();
        }
    }
}