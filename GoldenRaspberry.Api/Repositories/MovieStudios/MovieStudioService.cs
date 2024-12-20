using GoldenRaspberry.Api.Data;
using GoldenRaspberry.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GoldenRaspberry.Api.Repositories.MovieStudios
{
    public class MovieStudioRepository : IMovieStudioRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MovieStudioRepository> _logger;

        public MovieStudioRepository(AppDbContext context, ILogger<MovieStudioRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<MovieStudio>> GetMovieStudiosAsync()
        {
            try
            {
                _logger.LogInformation("Buscando todos os estúdios de filmes.");
                return await _context.MovieStudios.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar estúdios de filmes.");
                throw;
            }
        }
    }
}
