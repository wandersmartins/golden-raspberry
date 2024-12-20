using GoldenRaspberry.Api.Data;
using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberry.Api.Repositories.MovieProducers
{
    public class MovieProducerRepository : IMovieProducerRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MovieProducerRepository> _logger;

        public MovieProducerRepository(AppDbContext context, ILogger<MovieProducerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<MovieProducer>> GetMovieProducersAsync()
        {
            try
            {
                _logger.LogInformation("Buscando todos os produtores de filmes.");
                return await _context.MovieProducers.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar produtores de filmes.");
                throw;
            }
        }
        public async Task<ProducerIntervalResponseDto> GetProducersWithIntervalsAsync()
        {
            var moviesWithProducers = await _context.MovieProducers
                .Include(mp => mp.Movie)
                .Include(mp => mp.Producer)
                .Where(mp => mp.Movie.IsWinner)
                .ToListAsync();

            var producerIntervals = new Dictionary<int, List<Movie>>();

            foreach (var mp in moviesWithProducers)
            {
                if (!producerIntervals.ContainsKey(mp.Producer.Id))
                {
                    producerIntervals[mp.Producer.Id] = new List<Movie>();
                }
                producerIntervals[mp.Producer.Id].Add(mp.Movie);
            }

            var intervals = new List<ProducerIntervalDto>();

            foreach (var producerGroup in producerIntervals)
            {
                var orderedMovies = producerGroup.Value.OrderBy(m => m.Year).ToList();

                for (int i = 1; i < orderedMovies.Count; i++)
                {
                    var interval = orderedMovies[i].Year - orderedMovies[i - 1].Year;
                    _logger.LogInformation($"ProducerId: {producerGroup.Key}, Interval: {interval}, PreviousYear: {orderedMovies[i - 1].Year}, FollowingYear: {orderedMovies[i].Year}");

                    intervals.Add(new ProducerIntervalDto
                    {
                        ProducerName = moviesWithProducers.First(mp => mp.Producer.Id == producerGroup.Key).Producer.Name,
                        Interval = interval,
                        PreviousYear = orderedMovies[i - 1].Year,
                        FollowingYear = orderedMovies[i].Year
                    });
                }
            }

            var minInterval = intervals.OrderBy(i => i.Interval).FirstOrDefault();
            var maxInterval = intervals.OrderByDescending(i => i.Interval).FirstOrDefault();

            _logger.LogInformation($"MinInterval: {minInterval?.Interval}, MaxInterval: {maxInterval?.Interval}");

            return new ProducerIntervalResponseDto
            {
                Min = minInterval,
                Max = maxInterval
            };
        }
    }
}
