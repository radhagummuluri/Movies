using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Movies.RestApi.Adapters;
using Movies.RestApi.Data.Repositories;
using Movies.RestApi.Response;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.RestApi.Queries.SearchMovies
{
    public class SearchMoviesQueryHandler : IRequestHandler<SearchMoviesQuery, ValidatedResponse<ICollection<MovieDto>>>
    {
        private readonly IValidator<SearchMoviesQuery> _validator;
        private readonly IMovieRepository _movieRepository;

        public SearchMoviesQueryHandler(IValidator<SearchMoviesQuery> validator, IMovieRepository movieRepository)
        {
            _validator = validator;
            _movieRepository = movieRepository;
        }

        public async Task<ValidatedResponse<ICollection<MovieDto>>> Handle(SearchMoviesQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new ValidatedResponse<ICollection<MovieDto>>(validationResult, null);
            }

            var genres = !string.IsNullOrEmpty(request.Genres)
                ? request.Genres.Split(",").Select(x => x.Trim().ToLower()).ToList()
                : Enumerable.Empty<string>();

            var movies = await _movieRepository.SearchMovies(request.Title?.Trim().ToLower(), request.YearOfRelease, genres.ToHashSet());

            if (movies?.Count > 0)
            {
                return new ValidatedResponse<ICollection<MovieDto>>(validationResult, movies.ToMovieResponse());
            }

            var notFound = new ValidationFailure("Movie", "No movies found based on the given search criteria")
            {
                ErrorCode = HttpStatusCode.NotFound.ToString()
            };

            validationResult.Errors.Add(notFound);
            return new ValidatedResponse<ICollection<MovieDto>>(validationResult, null);
        }
    }
}
