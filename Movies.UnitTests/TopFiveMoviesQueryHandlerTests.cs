using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Movies.RestApi.Data.ReadModel;
using Movies.RestApi.Data.Repositories;
using Movies.RestApi.Queries.TopFiveMovies;
using Movies.RestApi.Response;
using Xunit;

namespace Movies.UnitTests
{
    public class TopFiveMoviesQueryHandlerTests
    {
        private readonly Mock<IValidator<TopFiveMoviesQuery>> _mockValidator;
        private readonly Mock<IMovieRepository> _mockMovieRepository;
        private readonly TopFiveMoviesQueryHandler _sut;

        public TopFiveMoviesQueryHandlerTests()
        {
            _mockValidator = new Mock<IValidator<TopFiveMoviesQuery>>();
            _mockMovieRepository = new Mock<IMovieRepository>();
            _sut = new TopFiveMoviesQueryHandler(_mockValidator.Object, _mockMovieRepository.Object);
        }

        [Fact]
        public async Task When_TopFiveMoviesQueried_Averages_Rounded_AwayFromZero()
        {
            var query = new TopFiveMoviesQuery();
            _mockValidator.Setup(x => x.Validate(query))
                .Returns(new ValidationResult());
            
            var movies = GetMovies();
            _mockMovieRepository.Setup(x => x.TopFiveMovies(query.UserId)).ReturnsAsync(movies);

            var response = await _sut.Handle(query, default);

            response.ValidationResult.IsValid.Should().BeTrue();
            response.Result.FirstOrDefault(x => x.ID == 1)?.AverageRating.Should().Be("4.0");

            var missSloan = response.Result.Select((c, i) => new {Index = i, Movie = c})
                .FirstOrDefault(x => x.Movie.ID == 2);
            missSloan?.Index.Should().Be(2);
            missSloan?.Movie.AverageRating.Should().Be("3.5");
            
            var br = response.Result.Select((c, i) => new { Index = i, Movie = c })
                .FirstOrDefault(x => x.Movie.ID == 1);
            missSloan?.Index.Should().Be(2);
            missSloan?.Movie.AverageRating.Should().Be("3.5");

            response.Result.FirstOrDefault(x => x.ID == 4)?.AverageRating.Should().Be("3.0");
            response.Result.FirstOrDefault(x => x.ID == 5)?.AverageRating.Should().Be("3.0");
        }

        private ICollection<MovieReadModel> GetMovies()
        {
            return new List<MovieReadModel>
            {
                new MovieReadModel {ID= 1, CalculatedAverageRating = 3.75, Genres = "Drama,Science Fiction", RunningTime = 130, Title = "The Man Who Fell To Earth", YearOfRelease = 1976},
                new MovieReadModel {ID= 2, CalculatedAverageRating = 3.6, Genres = "Drama,Thriller", RunningTime = 132, Title = "Miss Sloan", YearOfRelease = 2016},
                new MovieReadModel {ID= 3, CalculatedAverageRating = 3.25, Genres = "Music,Drama", RunningTime = 135, Title = "Bohemian Rhapsody", YearOfRelease = 2018},
                new MovieReadModel {ID= 4, CalculatedAverageRating = 3.249, Genres = "Comedy", RunningTime = 107, Title = "Dumb and Dumber", YearOfRelease = 1994},
                new MovieReadModel {ID= 5, CalculatedAverageRating = 2.91, Genres = "Comedy,Crime", RunningTime = 101, Title = "The Mask", YearOfRelease = 1994},
            };
        }
    }
}
