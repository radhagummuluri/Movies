using MediatR;
using Movies.RestApi.Response;

namespace Movies.RestApi.Commands.MovieRating
{
    public class UpsertMovieRatingCommand : IRequest<ValidatedResponse<UpsertMovieRatingCommandResponse>>
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int Rating { get; set; }
    }
}
