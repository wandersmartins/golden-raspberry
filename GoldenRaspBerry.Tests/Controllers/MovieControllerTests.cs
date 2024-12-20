using GoldenRaspberry.Api.Controllers;
using GoldenRaspberry.Api.Services.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GoldenRaspBerry.Tests.Controllers
{
    public class MoviesControllerTests
    {
        private readonly Mock<IMovieService> _movieServiceMock;
        private readonly Mock<ILogger<MoviesController>> _loggerMock;
        private readonly MoviesController _controller;

        public MoviesControllerTests()
        {
            _movieServiceMock = new Mock<IMovieService>();
            _loggerMock = new Mock<ILogger<MoviesController>>();
            _controller = new MoviesController(_movieServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetMovies_ReturnsOkResult()
        {
            // Arrange
            var filter = "Winner";
            var page = 1;
            var pageSize = 10;
            var movies = new
            {
                TotalCount = 2,
                Items = new List<object>
                {
                    new { Id = 1, Title = "Movie A", Year = 2021, IsWinner = true },
                    new { Id = 2, Title = "Movie B", Year = 2020, IsWinner = false }
                }
            };

            _movieServiceMock
                .Setup(service => service.GetMoviesAsync(filter, page, pageSize))
                .ReturnsAsync(movies);

            // Act
            var result = await _controller.GetMovies(filter, page, pageSize);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(movies, okResult.Value);
        }

        [Fact]
        public async Task GetMovies_ReturnsServerError_WhenExceptionOccurs()
        {
            // Arrange
            var filter = "Winner";
            var page = 1;
            var pageSize = 10;

            _movieServiceMock
                .Setup(service => service.GetMoviesAsync(filter, page, pageSize))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetMovies(filter, page, pageSize);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Contains("Erro ao buscar anos com múltiplos vencedores", statusCodeResult.Value.ToString());
        }
    }
}
