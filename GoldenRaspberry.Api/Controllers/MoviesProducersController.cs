using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Services.MovieProducers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GoldenRaspberry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesProducersController : ControllerBase
    {
        private readonly IMovieProducerService _movieProducerService;
        private readonly ILogger<MoviesProducersController> _logger;

        public MoviesProducersController(IMovieProducerService movieProducerService, ILogger<MoviesProducersController> logger)
        {
            _movieProducerService = movieProducerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieProducers()
        {
            try
            {
                var movieProducers = await _movieProducerService.GetMovieProducersAsync();
                return Ok(movieProducers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar o serviço: GetMovieProducersAsync.");
                return StatusCode(500, "Erro ao processar a solicitação.");
            }
        }
    }
}
