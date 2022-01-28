using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Movies.RestApi.Data.Repositories;
using Movies.RestApi.Response;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Movies.RestApi.Adapters;

namespace Movies.RestApi.Queries.TopFiveMovies
{
    public class TopFiveMoviesQueryHandler : IRequestHandler<TopFiveMoviesQuery, ValidatedResponse<ICollection<MovieDto>>>
    {
        private readonly IValidator<TopFiveMoviesQuery> _validator;
        private readonly IMovieRepository _movieRepository;

        public TopFiveMoviesQueryHandler(IValidator<TopFiveMoviesQuery> validator, IMovieRepository movieRepository)
        {
            _validator = validator;
            _movieRepository = movieRepository;
        }

        public async Task<ValidatedResponse<ICollection<MovieDto>>> Handle(TopFiveMoviesQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new ValidatedResponse<ICollection<MovieDto>>(validationResult, null);
            }

            var movies = await _movieRepository.TopFiveMovies(request.UserId);

            if (movies?.Count > 0)
            {
                var finalResponse = movies.ToMovieResponse().OrderByDescending(m => m.AverageRating)
                    .ThenBy(m => m.Title).ToList();
                return new ValidatedResponse<ICollection<MovieDto>>(validationResult, finalResponse);
            }

            var notFound = new ValidationFailure("Movie", $"No top five movies found {(request.UserId > 0 ? "for UserId " + request.UserId : string.Empty)}")
            {
                ErrorCode = HttpStatusCode.NotFound.ToString()
            };

            validationResult.Errors.Add(notFound);
            return new ValidatedResponse<ICollection<MovieDto>>(validationResult, null);
        }
    }
}
