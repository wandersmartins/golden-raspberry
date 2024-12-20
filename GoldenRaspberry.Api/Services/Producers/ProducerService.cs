using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Repositories.Producers;

namespace GoldenRaspberry.Api.Services.Producers
{
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;

        public ProducerService(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }

        public async Task<object> GetProducersAsync(string filter, int page, int pageSize)
        {
            return await _producerRepository.GetProducersAsync(filter, page, pageSize);
        }

    }
}
