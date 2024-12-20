using GoldenRaspberry.Api.Models;

namespace GoldenRaspberry.Api.Services.Producers
{
    public interface IProducerService
    {
        Task<object> GetProducersAsync(string filter, int page, int pageSize);
    }
}
