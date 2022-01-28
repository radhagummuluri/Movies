using Microsoft.EntityFrameworkCore;

namespace Movies.RestApi.Data.Models
{
    [Index(nameof(MovieID), nameof(GenreID))]
    public class MovieGenre
    {
        public int GenreID { get; set; }
        public int MovieID { get; set; }
        public Genre Genre { get; set; }
        public Movie Movie { get; set; }
    }
}
