using Movies.RestApi.Data.ReadModel;
using Movies.RestApi.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.RestApi.Adapters
{
    public static class MovieAdapter
    {
        public static ICollection<MovieDto> ToMovieResponse(this ICollection<MovieReadModel> movies)
        {
            return movies.Select(m =>
            {
                var md = new MovieDto
                { 
                    ID = m.ID,
                    AverageRating =
                        $"{(Math.Round(m.CalculatedAverageRating * 2, MidpointRounding.AwayFromZero) / 2) * 1.0:0.0}",
                    Genres = m.Genres,
                    RunningTime = m.RunningTime,
                    Title = m.Title,
                    YearOfRelease = m.YearOfRelease
                };

                return md;
            }).ToList();
        }
    }
}
