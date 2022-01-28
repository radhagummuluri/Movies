using System.Collections.Generic;

namespace Movies.RestApi.Data.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<MovieRating> MovieRatings { get; set; }
    }
}
