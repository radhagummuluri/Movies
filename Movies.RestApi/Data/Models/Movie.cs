using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Movies.RestApi.Data.Models
{
    [Index(nameof(Title), nameof(YearOfRelease))]
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }

        public ICollection<MovieRating> MovieRatings { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
