using GoldenRaspberry.Api.Controllers;
using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Services.Studios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GoldenRaspBerry.Tests.Controllers
{
    public class StudiosControllerTests
    {
        private readonly Mock<IStudioService> _studioServiceMock;
        private readonly Mock<ILogger<StudiosController>> _loggerMock;
        private readonly StudiosController _controller;

        public StudiosControllerTests()
        {
            _studioServiceMock = new Mock<IStudioService>();
            _loggerMock = new Mock<ILogger<StudiosController>>();
            _controller = new StudiosController(_studioServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetStudios_ReturnsOkResult()
        {
            // Arrange
            var studios = new List<Studio>
            {
                new Studio { Id = 1, Name = "Studio A" },
                new Studio { Id = 2, Name = "Studio B" }
            };

            _studioServiceMock
                .Setup(service => service.GetStudiosAsync())
                .ReturnsAsync(studios);

            // Act
            var result = await _controller.GetStudios();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(studios, okResult.Value);
        }

        [Fact]
        public async Task GetStudios_ReturnsServerError_WhenExceptionOccurs()
        {
            // Arrange
            _studioServiceMock
                .Setup(service => service.GetStudiosAsync())
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetStudios();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Contains("Erro ao processar a solicitação", statusCodeResult.Value.ToString());
        }
    }
}
