using GoldenRaspberry.Api.Data;
using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            // Obter todos os filmes vencedores com seus produtores
            var moviesWithProducers = await _context.MovieProducers
                .Include(mp => mp.Movie)
                .Include(mp => mp.Producer)
                .Where(mp => mp.Movie.IsWinner)
                .ToListAsync();

            // Log dos dados obtidos
            foreach (var mp in moviesWithProducers)
            {
                _logger.LogInformation($"MovieId: {mp.MovieId}, ProducerId: {mp.ProducerId}, ProducerName: {mp.Producer.Name}, MovieTitle: {mp.Movie.Title}, Year: {mp.Movie.Year}, IsWinner: {mp.Movie.IsWinner}");
            }

            // Dicionário para agrupar produtores
            var producerIntervals = new Dictionary<int, List<Movie>>();

            foreach (var mp in moviesWithProducers)
            {
                if (!producerIntervals.ContainsKey(mp.Producer.Id))
                {
                    producerIntervals[mp.Producer.Id] = new List<Movie>();
                }
                producerIntervals[mp.Producer.Id].Add(mp.Movie);
            }

            // Verificar se o agrupamento foi feito corretamente
            foreach (var group in producerIntervals)
            {
                _logger.LogInformation($"ProducerId: {group.Key}, MovieCount: {group.Value.Count}");
            }

            // Lista para armazenar os resultados
            var intervals = new List<ProducerIntervalDto>();

            foreach (var producerGroup in producerIntervals)
            {
                var orderedMovies = producerGroup.Value.OrderBy(m => m.Year).ToList();

                // Depuração dos filmes ordenados
                _logger.LogInformation($"ProducerId: {producerGroup.Key}, OrderedMovies: {string.Join(", ", orderedMovies.Select(m => $"{m.Title} ({m.Year})"))}");

                // Calcular intervalos
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

            // Depuração dos intervalos calculados
            foreach (var interval in intervals)
            {
                _logger.LogInformation($"ProducerName: {interval.ProducerName}, Interval: {interval.Interval}, PreviousYear: {interval.PreviousYear}, FollowingYear: {interval.FollowingYear}");
            }

            // Identificar o intervalo mínimo e máximo
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
