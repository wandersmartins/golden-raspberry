using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Models.Dtos;
using GoldenRaspberry.Api.Repositories.MovieProducers;

namespace GoldenRaspberry.Api.Services.MovieProducers
{
    public class MovieProducerService : IMovieProducerService
    {
        private readonly IMovieProducerRepository _movieProducerRepository;

        public MovieProducerService(IMovieProducerRepository movieProducerRepository)
        {
            _movieProducerRepository = movieProducerRepository;
        }

        public async Task<List<MovieProducer>> GetMovieProducersAsync()
        {
            return await _movieProducerRepository.GetMovieProducersAsync();
        }

        public async Task<ProducerIntervalResponseDto> GetProducersWithIntervalsAsync()
        {
            return await _movieProducerRepository.GetProducersWithIntervalsAsync();
        }
    }
}
