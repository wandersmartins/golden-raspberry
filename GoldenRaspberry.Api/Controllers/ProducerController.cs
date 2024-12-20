using GoldenRaspberry.Api.Services.Producers;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberry.Api.Controllers
{
    [ApiController]
    [Route("api/producers")]
    public class ProducersController : ControllerBase
    {
        private readonly IProducerService _producerService;
        private readonly ILogger<ProducersController> _logger;

        public ProducersController(IProducerService producerService, ILogger<ProducersController> logger)
        {
            _producerService = producerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducers([FromQuery] string filter = "", [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _producerService.GetProducersAsync(filter, page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar o serviço: GetProducersAsync.");
                return StatusCode(500, $"Erro ao buscar anos com múltiplos vencedores: {ex.Message}");
            }
        }
    }
}
