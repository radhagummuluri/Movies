using FluentValidation;
using Movies.RestApi.Data.Repositories;
using Movies.RestApi.Queries.TopFiveMovies;
using System.Net;

namespace Movies.RestApi.Validators
{
    public class TopFiveMoviesQueryValidator : AbstractValidator<TopFiveMoviesQuery>
    {
        private readonly IMovieRepository _movieRepository;

        public TopFiveMoviesQueryValidator(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;

            RuleFor(prop => prop.UserId)
                .Must(BeValidIfSpecified)
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("UserId is invalid");
        }

        private bool BeValidIfSpecified(int? userId)
        {
            return (!userId.HasValue || userId.HasValue && userId.Value > 0 && _movieRepository.IsValidUser(userId.Value));
        }
    }
}
