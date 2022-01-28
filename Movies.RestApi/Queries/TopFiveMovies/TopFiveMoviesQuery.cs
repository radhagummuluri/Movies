using MediatR;
using Movies.RestApi.Response;
using System.Collections.Generic;

namespace Movies.RestApi.Queries.TopFiveMovies
{
    public class TopFiveMoviesQuery : IRequest<ValidatedResponse<ICollection<MovieDto>>>
    {
        public int? UserId { get; set; }
    }
}
