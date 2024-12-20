using GoldenRaspberry.Api.Services.Studios;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudiosController : ControllerBase
    {
        private readonly IStudioService _studioService;
        private readonly ILogger<StudiosController> _logger;

        public StudiosController(IStudioService studioService, ILogger<StudiosController> logger)
        {
            _studioService = studioService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudios()
        {
            try
            {
                var studios = await _studioService.GetStudiosAsync();
                return Ok(studios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao chamar o serviço: GetStudiosAsync.");
                return StatusCode(500, "Erro ao processar a solicitação.");
            }
        }
    }
}
