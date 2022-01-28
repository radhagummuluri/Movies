using MediatR;
using Movies.RestApi.Response;
using System.Collections.Generic;

namespace Movies.RestApi.Queries.SearchMovies
{
    public class SearchMoviesQuery : IRequest<ValidatedResponse<ICollection<MovieDto>>>
    {
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public string Genres { get; set; }
    }
}
