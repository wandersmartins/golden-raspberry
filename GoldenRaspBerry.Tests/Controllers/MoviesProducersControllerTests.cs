using GoldenRaspberry.Api.Controllers;
using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Services.MovieProducers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GoldenRaspBerry.Tests.Controllers
{
    public class MoviesProducersControllerTests
    {
        private readonly Mock<IMovieProducerService> _movieProducerServiceMock;
        private readonly Mock<ILogger<MoviesProducersController>> _loggerMock;
        private readonly MoviesProducersController _controller;

        public MoviesProducersControllerTests()
        {
            _movieProducerServiceMock = new Mock<IMovieProducerService>();
            _loggerMock = new Mock<ILogger<MoviesProducersController>>();
            _controller = new MoviesProducersController(_movieProducerServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetMovieProducers_ReturnsOkResult()
        {
            // Arrange
            var movieProducers = new List<MovieProducer>
            {
                new MovieProducer
                {
                    Id = 1,
                    MovieId = 1,
                    ProducerId = 1,
                    Movie = new Movie { Id = 1, Title = "Movie A", Year = 2021, IsWinner = true },
                    Producer = new Producer { Id = 1, Name = "Producer A" }
                },
                new MovieProducer
                {
                    Id = 2,
                    MovieId = 2,
                    ProducerId = 2,
                    Movie = new Movie { Id = 2, Title = "Movie B", Year = 2020, IsWinner = false },
                    Producer = new Producer { Id = 2, Name = "Producer B" }
                }
            };

            _movieProducerServiceMock
                .Setup(service => service.GetMovieProducersAsync())
                .ReturnsAsync(movieProducers);

            // Act
            var result = await _controller.GetMovieProducers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(movieProducers, okResult.Value);
        }

        [Fact]
        public async Task GetMovieProducers_ReturnsServerError_WhenExceptionOccurs()
        {
            // Arrange
            _movieProducerServiceMock
                .Setup(service => service.GetMovieProducersAsync())
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetMovieProducers();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Erro ao processar a solicitação.", statusCodeResult.Value);
        }
    }
}
