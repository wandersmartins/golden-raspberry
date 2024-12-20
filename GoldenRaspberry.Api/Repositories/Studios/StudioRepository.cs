using GoldenRaspberry.Api.Data;
using GoldenRaspberry.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberry.Api.Repositories.Studios
{
    public class StudioRepository : IStudioRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<StudioRepository> _logger;

        public StudioRepository(AppDbContext context, ILogger<StudioRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Studio>> GetStudiosAsync()
        {
            try
            {
                return await _context.Studios.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar os estúdios.");
                throw;
            }
        }

        public async Task<IEnumerable<object>> GetTopStudiosAsync()
        {
            return await _context.MovieStudios
                .Include(ms => ms.Studio)
                .Include(ms => ms.Movie)
                .Where(ms => ms.Movie.IsWinner)
                .GroupBy(ms => ms.Studio.Name)
                .Select(g => new
                {
                    Studio = g.Key,
                    WinnerCount = g.Count()
                })
                .OrderByDescending(g => g.WinnerCount)
                .Take(3)
                .ToListAsync();
        }

    }
}
