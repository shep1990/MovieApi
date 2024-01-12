using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieApi.Data.Interfaces;
using MovieApi.Data.Models;
using MovieApi.Handlers;
using MovieApi.Queries;

namespace MovieApi.Tests
{
    [TestClass]
    public class GetMovieQueryHandlerTests
    {
        private Mock<IMovieRepository> _movieRepository;
        private Mock<ILogger<GetMovieQueryHandler>> _logger;

        [TestInitialize]
        public void Setup()
        {
            _movieRepository = new Mock<IMovieRepository>();
            _logger = new Mock<ILogger<GetMovieQueryHandler>>();
        }

        [TestMethod]
        public async Task WhenTheQueryHandlerIsTriggered_AndAnExceptionOccurs_LogTheException()
        {
            _movieRepository.Setup(x => x.Get()).Throws(new Exception("Exception Thrown"));
            var sut = new GetMovieQueryHandler(_movieRepository.Object, _logger.Object);
            var movieQuery = new GetMovieQuery();

            var result = await sut.Handle(movieQuery, default);

            result.Success.Should().BeFalse();
            VerifyLogger("There was an issue with the request", LogLevel.Error);
        }

        [TestMethod]
        public async Task WhenTheQueryHandlerIsTriggered_AndThereAreItemsInTheDatabase_ThenThoseItemsShouldBeFound()
        {
            _movieRepository.Setup(x => x.Get()).ReturnsAsync(GetMovieList());
            var sut = new GetMovieQueryHandler(_movieRepository.Object, _logger.Object);
            var movieQuery = new GetMovieQuery();

            var result = await sut.Handle(movieQuery, default);


            result.Data.Count.Should().Be(3);
            CompareMovieLists(GetMovieList(), result.Data).Count.Should().Be(3);
        }

        private List<Movie> CompareMovieLists(List<Movie> expected, List<Movie> actual)
        {
            return expected.Where(e => actual.Any(a =>
                e.Genre == a.Genre &&
                e.Original_Language == a.Original_Language &&
                e.Title == a.Title &&
                e.Genre == a.Genre &&
                e.Poster_Url == a.Poster_Url &&
                e.Vote_Count == a.Vote_Count &&
                e.Overview == a.Overview &&
                e.Popularity == a.Popularity &&
                e.Release_Date == a.Release_Date &&
                e.Vote_Average == a.Vote_Average)).ToList();
        }

        private List<Movie> GetMovieList()
        {
            return new List<Movie>
            {
                new Movie
                {
                    Genre = "Horror",
                    Release_Date = DateTime.Now.AddYears(-32).Date,
                    Original_Language = "en",
                    Overview = "This is an overview for Silence of The Lambs",
                    Popularity = 7,
                    Poster_Url = "PosterUrl",
                    Title = "Silence of The Lambs",
                    Vote_Average = 3,
                    Vote_Count = 100
                },
                new Movie
                {
                    Genre = "Sci-fi",
                    Release_Date = DateTime.Now.AddYears(-27).Date,
                    Original_Language = "en",
                    Overview = "This is an overview for The Fith Element",
                    Popularity = 8,
                    Poster_Url = "PosterUrl",
                    Title = "The Fith Element",
                    Vote_Average = 4,
                    Vote_Count = 98
                },
                new Movie
                {
                    Genre = "Lord Of The Rings",
                    Release_Date = DateTime.Now.AddYears(-20).Date,
                    Original_Language = "en",
                    Overview = "This is an overview for The Fith Element",
                    Popularity = 10,
                    Poster_Url = "PosterUrl",
                    Title = "The Fith Element",
                    Vote_Average = 10,
                    Vote_Count = 100
                }
            };
        }

        private void VerifyLogger(string message, LogLevel logLevel)
        {
            _logger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == logLevel),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == message),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }
}