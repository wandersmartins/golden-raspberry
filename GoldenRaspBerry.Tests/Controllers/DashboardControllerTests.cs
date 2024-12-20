using GoldenRaspberry.Api.Controllers;
using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Models.Dtos;
using GoldenRaspberry.Api.Services.MovieProducers;
using GoldenRaspberry.Api.Services.Movies;
using GoldenRaspberry.Api.Services.Producers;
using GoldenRaspberry.Api.Services.Studios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GoldenRaspBerry.Tests.Controllers
{
    public class DashboardControllerTests
    {
        private readonly Mock<IMovieService> _movieServiceMock;
        private readonly Mock<IMovieProducerService> _movieProducerServiceMock;
        private readonly Mock<IProducerService> _producerServiceMock;
        private readonly Mock<IStudioService> _studioServiceMock;
        private readonly Mock<ILogger<StudiosController>> _loggerMock;
        private readonly DashboardController _controller;

        public DashboardControllerTests()
        {
            _movieServiceMock = new Mock<IMovieService>();
            _movieProducerServiceMock = new Mock<IMovieProducerService>();
            _producerServiceMock = new Mock<IProducerService>();
            _studioServiceMock = new Mock<IStudioService>();
            _loggerMock = new Mock<ILogger<StudiosController>>();

            _controller = new DashboardController(
                _loggerMock.Object,
                _movieServiceMock.Object,
                _movieProducerServiceMock.Object,
                _producerServiceMock.Object,
                _studioServiceMock.Object
            );
        }

               [Fact]
        public async Task GetTopStudios_ReturnsOkResult()
        {
            // Arrange
            var studios = new List<object>
            {
                new { Studio = "Studio A", WinCount = 5 },
                new { Studio = "Studio B", WinCount = 3 }
            };
            _studioServiceMock
                .Setup(service => service.GetTopStudiosAsync())
                .ReturnsAsync(studios.AsEnumerable());

            // Act
            var result = await _controller.GetTopStudios();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(studios, okResult.Value);
        }

        [Fact]
        public async Task GetAvailableYears_ReturnsOkResult()
        {
            // Arrange
            var years = new List<int> { 1980, 1990, 2000 };
            _movieServiceMock
                .Setup(service => service.GetAvailableYearsAsync())
                .ReturnsAsync(years.AsEnumerable());

            // Act
            var result = await _controller.GetAvailableYears();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(years, okResult.Value);
        }

        [Fact]
        public async Task GetWinnersByYear_ReturnsOkResult()
        {
            // Arrange
            var winners = new List<object>
            {
                new { Title = "Movie A", Producer = "Producer A", Studio = "Studio A", Year = 1980 },
                new { Title = "Movie B", Producer = "Producer B", Studio = "Studio B", Year = 1990 }
            };
            var year = 1980;

            _movieServiceMock
                .Setup(service => service.GetWinnersByYearAsync(year))
                .ReturnsAsync(winners.AsEnumerable());

            // Act
            var result = await _controller.GetWinnersByYear(year);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(winners, okResult.Value);
        }
    }
}
