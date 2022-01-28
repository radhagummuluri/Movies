using System.Net;
using FluentValidation;
using Movies.RestApi.Commands.MovieRating;
using Movies.RestApi.Data.Repositories;

namespace Movies.RestApi.Validators
{
    public class UpsertMovieRatingCommandValidator : AbstractValidator<UpsertMovieRatingCommand>
    {
        private readonly IMovieRepository _movieRepository;

        public UpsertMovieRatingCommandValidator(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;

            RuleFor(prop => prop.UserId)
                .Must(BeValidUser)
                .WithErrorCode(HttpStatusCode.NotFound.ToString())
                .WithMessage("User not found");

            RuleFor(prop => prop.MovieId)
                .Must(BeValidMovie)
                .WithErrorCode(HttpStatusCode.NotFound.ToString())
                .WithMessage("Movie not found");

            RuleFor(prop => prop.Rating)
                .InclusiveBetween(1,5)
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("Rating is an invalid value");
        }

        private bool BeValidUser(int userId)
        {
            return (userId > 0 && _movieRepository.IsValidUser(userId));
        }

        private bool BeValidMovie(int movieId)
        {
            return (movieId > 0 && _movieRepository.IsValidMovie(movieId));
        }
    }
}
