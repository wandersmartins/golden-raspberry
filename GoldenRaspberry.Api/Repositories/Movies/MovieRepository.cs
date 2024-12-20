using GoldenRaspberry.Api.Data;
using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Repositories.Producers;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberry.Api.Repositories.Movies
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MovieRepository> _logger;

        public MovieRepository(AppDbContext context, ILogger<MovieRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<object> GetMoviesAsync(string filter, int page, int pageSize)
        {
            var query = _context.Movies
                .Where(m => string.IsNullOrEmpty(filter) || m.Title.Contains(filter))
                .OrderBy(m => m.Year);

            var total = await query.CountAsync();
            var items = await query
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(m => new { m.Title, m.Year, m.IsWinner })
                .ToListAsync();

            return new { total, items };
        }

        public async Task<IEnumerable<object>> GetYearsWithMultipleWinnersAsync()
        {
            try
            {
                var movies = await _context.Movies.Where(m => m.IsWinner).ToListAsync();
                _logger.LogInformation($"Found {movies.Count} winning movies.");

                var grouped = movies
                    .GroupBy(m => m.Year)
                    .Where(g => g.Count() > 1)
                    .Select(g => new
                    {
                        Year = g.Key,
                        WinnerCount = g.Count()
                    })
                    .ToList();

                _logger.LogInformation($"Found {grouped.Count} years with multiple winners.");
                return grouped;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching years with multiple winners.");
                throw;
            }
        }

        public async Task<IEnumerable<int>> GetAvailableYearsAsync()
        {
            return await _context.Movies
                .Select(m => m.Year)
                .Distinct()
                .OrderBy(year => year)
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetWinnersByYearAsync(int year)
        {
            return await _context.Movies
                .Where(m => m.Year == year && m.IsWinner)
                .Select(m => new
                {
                    Title = m.Title,
                    Producers = m.MovieProducers.Select(mp => mp.Producer.Name).ToList(),
                    Studios = m.MovieStudios.Select(ms => ms.Studio.Name).ToList()
                })
                .ToListAsync();
        }

    }
}
