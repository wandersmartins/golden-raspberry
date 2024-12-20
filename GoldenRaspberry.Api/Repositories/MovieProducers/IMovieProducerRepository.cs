using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Models.Dtos;

namespace GoldenRaspberry.Api.Repositories.MovieProducers
{
    public interface IMovieProducerRepository
    {
        Task<List<MovieProducer>> GetMovieProducersAsync();
        Task<ProducerIntervalResponseDto> GetProducersWithIntervalsAsync();
    }
}
