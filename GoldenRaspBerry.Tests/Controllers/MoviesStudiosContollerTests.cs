using GoldenRaspberry.Api.Controllers;
using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Services.MovieStudios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GoldenRaspBerry.Tests.Controllers
{
    public class MovieStudiosControllerTests
    {
        private readonly Mock<IMovieStudioService> _movieStudioServiceMock;
        private readonly Mock<ILogger<MovieStudiosController>> _loggerMock;
        private readonly MovieStudiosController _controller;

        public MovieStudiosControllerTests()
        {
            _movieStudioServiceMock = new Mock<IMovieStudioService>();
            _loggerMock = new Mock<ILogger<MovieStudiosController>>();
            _controller = new MovieStudiosController(_movieStudioServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetMovieStudios_ReturnsOkResult()
        {
            // Arrange
            var movieStudios = new List<MovieStudio>
            {
                new MovieStudio
                {
                    Id = 1,
                    MovieId = 1,
                    StudioId = 1,
                    Movie = new Movie { Id = 1, Title = "Movie A", Year = 2021, IsWinner = true },
                    Studio = new Studio { Id = 1, Name = "Studio A" }
                },
                new MovieStudio
                {
                    Id = 2,
                    MovieId = 2,
                    StudioId = 2,
                    Movie = new Movie { Id = 2, Title = "Movie B", Year = 2020, IsWinner = false },
                    Studio = new Studio { Id = 2, Name = "Studio B" }
                }
            };

            _movieStudioServiceMock
                .Setup(service => service.GetMovieStudiosAsync())
                .ReturnsAsync(movieStudios);

            // Act
            var result = await _controller.GetMovieStudios();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(movieStudios, okResult.Value);
        }

        [Fact]
        public async Task GetMovieStudios_ReturnsServerError_WhenExceptionOccurs()
        {
            // Arrange
            _movieStudioServiceMock
                .Setup(service => service.GetMovieStudiosAsync())
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetMovieStudios();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Erro ao processar a solicitação.", statusCodeResult.Value);
        }
    }
}
