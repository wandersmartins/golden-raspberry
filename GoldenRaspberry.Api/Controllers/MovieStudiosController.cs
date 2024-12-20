using GoldenRaspberry.Api.Services.MovieStudios;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieStudiosController : ControllerBase
    {
        private readonly IMovieStudioService _movieStudioService;
        private readonly ILogger<MovieStudiosController> _logger;

        public MovieStudiosController(IMovieStudioService movieStudioService, ILogger<MovieStudiosController> logger)
        {
            _movieStudioService = movieStudioService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieStudios()
        {
            try
            {
                var movieStudios = await _movieStudioService.GetMovieStudiosAsync();
                return Ok(movieStudios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar o serviço: GetMovieStudiosAsync.");
                return StatusCode(500, "Erro ao processar a solicitação.");
            }
        }
    }
}
