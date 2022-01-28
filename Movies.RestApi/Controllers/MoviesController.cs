using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movies.RestApi.Queries.SearchMovies;
using Movies.RestApi.Queries.TopFiveMovies;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using Movies.RestApi.Commands.MovieRating;

namespace Movies.RestApi.Controllers
{
    [ApiController]
    [Route("v1/movies")]
    public class MoviesController : Controller
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchMoviesQuery query)
        {
            var response = await _mediator.Send(query);
            if (!response.ValidationResult.IsValid)
            {
                return ReturnBasedOnValidationResult(response.ValidationResult);
            }
            return Ok(response.Result);
        }

        [HttpGet("topFive")]
        public async Task<IActionResult> TopFiveMovies([FromQuery] TopFiveMoviesQuery query)
        {
            var response = await _mediator.Send(query);
            if (!response.ValidationResult.IsValid)
            {
                return ReturnBasedOnValidationResult(response.ValidationResult);
            }
            return Ok(response.Result);
        }

        [HttpPost("movieRating")]
        public async Task<IActionResult> AddOrUpdateMovieRating([FromBody] UpsertMovieRatingCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.ValidationResult.IsValid)
            {
                return ReturnBasedOnValidationResult(response.ValidationResult);
            }
            return Ok(response.Result);
        }

        private IActionResult ReturnBasedOnValidationResult(ValidationResult result)
        {
            var notFoundValidationResult =
                result?.Errors.FirstOrDefault(x =>
                    x.ErrorCode == HttpStatusCode.NotFound.ToString());
            if (notFoundValidationResult != null)
            {
                return NotFound(notFoundValidationResult.ErrorMessage);
            }

            var badRequestValidationResult =
                result?.Errors.FirstOrDefault(x =>
                    x.ErrorCode == HttpStatusCode.BadRequest.ToString());
            if (badRequestValidationResult != null)
            {
                return BadRequest(badRequestValidationResult.ErrorMessage);
            }

            return BadRequest(result);
        }
    }
}
