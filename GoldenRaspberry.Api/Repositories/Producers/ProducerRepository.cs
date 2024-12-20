using GoldenRaspberry.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberry.Api.Repositories.Producers
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly ILogger<ProducerRepository> _logger;
        private readonly AppDbContext _context;

        public ProducerRepository(ILogger<ProducerRepository> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<object> GetProducersAsync(string filter, int page, int pageSize)
        {
            var query = _context.Producers
                .Where(m => string.IsNullOrEmpty(filter) || m.Name.Contains(filter))
                .OrderBy(m => m.Name);

            var total = await query.CountAsync();
            var items = await query
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(m => new { m.Name })
                .ToListAsync();

            return new { total, items };
        }
    }
}
