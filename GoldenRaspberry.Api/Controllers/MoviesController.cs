using GoldenRaspberry.Api.Services.Movies;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberry.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(IMovieService movieService, ILogger<MoviesController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] string filter = "", [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _movieService.GetMoviesAsync(filter, page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar o serviço: GetMoviesAsync.");
                return StatusCode(500, $"Erro ao buscar anos com múltiplos vencedores: {ex.Message}");
            }
        }

    }
}
