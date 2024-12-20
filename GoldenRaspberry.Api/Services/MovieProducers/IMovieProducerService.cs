using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Models.Dtos;

namespace GoldenRaspberry.Api.Services.MovieProducers
{
    public interface IMovieProducerService
    {
        Task<List<MovieProducer>> GetMovieProducersAsync();
        Task<ProducerIntervalResponseDto> GetProducersWithIntervalsAsync();
    }
}
