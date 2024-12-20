using GoldenRaspberry.Api.Controllers;
using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Services.Producers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GoldenRaspBerry.Tests.Controllers
{
    public class ProducersControllerTests
    {
        private readonly Mock<IProducerService> _producerServiceMock;
        private readonly Mock<ILogger<ProducersController>> _loggerMock;
        private readonly ProducersController _controller;

        public ProducersControllerTests()
        {
            _producerServiceMock = new Mock<IProducerService>();
            _loggerMock = new Mock<ILogger<ProducersController>>();
            _controller = new ProducersController(_producerServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetProducers_ReturnsOkResult()
        {
            // Arrange
            var producers = new List<Producer>
            {
                new Producer { Id = 1, Name = "Producer A" },
                new Producer { Id = 2, Name = "Producer B" }
            };

            var resultMock = new
            {
                Total = 2,
                Items = producers
            };

            _producerServiceMock
                .Setup(service => service.GetProducersAsync("", 0, 10))
                .ReturnsAsync(resultMock);

            // Act
            var result = await _controller.GetProducers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(resultMock, okResult.Value);
        }

        [Fact]
        public async Task GetProducers_ReturnsServerError_WhenExceptionOccurs()
        {
            // Arrange
            _producerServiceMock
                .Setup(service => service.GetProducersAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetProducers();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Contains("Erro ao buscar anos com múltiplos vencedores", statusCodeResult.Value.ToString());
        }
    }
}
