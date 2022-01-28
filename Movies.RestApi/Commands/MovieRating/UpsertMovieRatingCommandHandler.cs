using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Movies.RestApi.Data.Repositories;
using Movies.RestApi.Response;

namespace Movies.RestApi.Commands.MovieRating
{
    public class UpsertMovieRatingCommandHandler : IRequestHandler<UpsertMovieRatingCommand, ValidatedResponse<UpsertMovieRatingCommandResponse>>
    {
        private readonly IValidator<UpsertMovieRatingCommand> _validator;
        private readonly IMovieRepository _movieRepository;

        public UpsertMovieRatingCommandHandler(IValidator<UpsertMovieRatingCommand> validator, IMovieRepository movieRepository)
        {
            _validator = validator;
            _movieRepository = movieRepository;
        }

        public async Task<ValidatedResponse<UpsertMovieRatingCommandResponse>> Handle(UpsertMovieRatingCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new ValidatedResponse<UpsertMovieRatingCommandResponse>(validationResult, null);
            }

            await _movieRepository.UpsertMovieRating(request.UserId, request.MovieId, request.Rating);

            return new ValidatedResponse<UpsertMovieRatingCommandResponse>(validationResult, new UpsertMovieRatingCommandResponse {Success = true});
        }
    }
}
