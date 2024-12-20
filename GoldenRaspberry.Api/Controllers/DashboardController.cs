using GoldenRaspberry.Api.Models.Dtos;
using GoldenRaspberry.Api.Services.MovieProducers;
using GoldenRaspberry.Api.Services.Movies;
using GoldenRaspberry.Api.Services.Producers;
using GoldenRaspberry.Api.Services.Studios;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberry.Api.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMovieProducerService _movieProducerService;
        private readonly IProducerService _producerService;
        private readonly IStudioService _studioService;
        private readonly ILogger<StudiosController> _logger;

        public DashboardController(ILogger<StudiosController> logger,
            IMovieService movieService,
            IMovieProducerService movieProducerService,
            IProducerService producerService,
            IStudioService studioService)
        {
            _logger = logger;
            _movieService = movieService;
            _movieProducerService = movieProducerService;
            _producerService = producerService;
            _studioService = studioService;
        }

        [HttpGet("multiple-winners")]
        public async Task<IActionResult> GetYearsWithMultipleWinners()
        {
            try
            {
                var years = await _movieService.GetYearsWithMultipleWinnersAsync();
                return Ok(years);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar o serviço: GetYearsWithMultipleWinnersAsync.");
                return StatusCode(500, $"Erro ao buscar anos com múltiplos vencedores: {ex.Message}");
            }
        }

        [HttpGet("top-studios")]
        public async Task<IActionResult> GetTopStudios()
        {
            try
            {
                var studios = await _studioService.GetTopStudiosAsync();
                return Ok(studios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar o serviço: GetTopStudiosAsync.");
                return StatusCode(500, "Erro ao processar a solicitação.");
            }
        }

        [HttpGet("producers-intervals")]
        public async Task<ActionResult<ProducerIntervalResponseDto>> GetProducersWithIntervals()
        {
            try
            {
                var result = await _movieProducerService.GetProducersWithIntervalsAsync();

                if (result.Min == null || result.Max == null)
                {
                    return NotFound("No intervals found for producers.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar o serviço: GetProducersWithIntervalsAsync.");
                return StatusCode(500, "Erro ao processar a solicitação.");
            }
        }


        [HttpGet("available-years")]
        public async Task<IActionResult> GetAvailableYears()
        {
            try
            {
                var years = await _movieService.GetAvailableYearsAsync();
                return Ok(years);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar o serviço: GetAvailableYearsAsync.");
                return StatusCode(500, "Erro ao processar a solicitação.");
            }
        }

        [HttpGet("winners-by-year/{year}")]
        public async Task<IActionResult> GetWinnersByYear(int year)
        {
            try
            {
                var winners = await _movieService.GetWinnersByYearAsync(year);
                return Ok(winners);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar o serviço: GetWinnersByYearAsync.");
                return StatusCode(500, "Erro ao processar a solicitação.");
            }
        }
    }

}
