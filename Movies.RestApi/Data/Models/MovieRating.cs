using Microsoft.EntityFrameworkCore;

namespace Movies.RestApi.Data.Models
{
    [Index(nameof(UserID))]
    public class MovieRating
    {
        public int UserID { get; set; }
        public int MovieID { get; set; }

        public int Rating { get; set; }

        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
