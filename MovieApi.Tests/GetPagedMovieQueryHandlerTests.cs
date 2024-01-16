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
    public class GetPagedMovieQueryHandlerTests
    {
        private Mock<IMovieRepository> _movieRepository;
        private Mock<ILogger<GetPagedMovieQueryHandler>> _logger;

        [TestInitialize]
        public void Setup()
        {
            _movieRepository = new Mock<IMovieRepository>();
            _logger = new Mock<ILogger<GetPagedMovieQueryHandler>>();
        }

        [TestMethod]
        public async Task WhenTheQueryHandlerIsTriggered_AndAnExceptionOccurs_LogTheException()
        {
            _movieRepository.Setup(x => x.GetByPageNumber(1, 10, null)).Throws(new Exception("Exception Thrown"));
            var sut = new GetPagedMovieQueryHandler(_movieRepository.Object, _logger.Object);
            var movieQuery = new GetPagedMovieQuery(1, 10, null);

            var result = await sut.Handle(movieQuery, default);

            result.Success.Should().BeFalse();
            VerifyLogger("There was an issue with the request", LogLevel.Error);
        }

        [TestMethod]
        [DataRow(5, 20)]
        [DataRow(10, 45)]
        [DataRow(15, 75)]
        public async Task WhenTheQueryHandlerIsTriggered_AndThereAreItemsInTheDatabase_ThenThoseItemsShouldBeFound(int pageTotal, int overallTotal)
        {
            _movieRepository.Setup(x => x.Get()).ReturnsAsync(GetMovieList(overallTotal));
            _movieRepository.Setup(x => x.GetByPageNumber(1, pageTotal, null)).ReturnsAsync(GetMovieList(pageTotal));
            var sut = new GetPagedMovieQueryHandler(_movieRepository.Object, _logger.Object);
            var movieQuery = new GetPagedMovieQuery(1, pageTotal, null);

            var result = await sut.Handle(movieQuery, default);

            result.Data.Count.Should().Be(pageTotal);
            result.Count.Should().Be(overallTotal);
            result.PageSize.Should().Be(pageTotal);
            result.PageIndex.Should().Be(1);
            CompareMovieLists(GetMovieList(pageTotal), result.Data).Count.Should().Be(pageTotal);
        }

        [TestMethod]
        [DataRow("Movie Name 0")]
        public async Task WhenFilteringResults_ThenTheCorrectRecordShouldBeReturned(string title)
        {
            _movieRepository.Setup(x => x.Get()).ReturnsAsync(GetMovieList(1));
            _movieRepository.Setup(x => x.GetByPageNumber(1, 1, title)).ReturnsAsync(GetMovieList(1));
            var sut = new GetPagedMovieQueryHandler(_movieRepository.Object, _logger.Object);
            var movieQuery = new GetPagedMovieQuery(1, 1, title);

            var result = await sut.Handle(movieQuery, default);

            result.Data.Count.Should().Be(1);
            result.PageIndex.Should().Be(1);
            result.Data.First().Title.Should().Be(title);
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

        private List<Movie> GetMovieList(int pageTotal)
        {

            var movieList = new List<Movie>();

            for(int i = 0; i < pageTotal; i++)
            {
                movieList.Add(new Movie
                {
                    Genre = "Horror",
                    Release_Date = DateTime.Now.AddYears(-32).Date,
                    Original_Language = "en",
                    Overview = string.Format("This is an overview for Movie Name {0}", i),
                    Popularity = 7,
                    Poster_Url = "PosterUrl",
                    Title = string.Format("Movie Name {0}", i),
                    Vote_Average = 3,
                    Vote_Count = 100
                });
            }

            return movieList;
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