using FluentValidation;
using Movies.RestApi.Queries.SearchMovies;
using System.Net;

namespace Movies.RestApi.Validators
{
    public class SearchMoviesQueryValidator : AbstractValidator<SearchMoviesQuery>
    {
        public SearchMoviesQueryValidator()
        {
            RuleFor(prop => prop)
                .Must(HaveAtleastOneFilter)
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("Must specify atleast one filter value");
        }

        private bool HaveAtleastOneFilter(SearchMoviesQuery query)
        {
            return (!string.IsNullOrEmpty(query.Title) || query.YearOfRelease > 0 || !string.IsNullOrEmpty(query.Genres));
        }
    }
}
