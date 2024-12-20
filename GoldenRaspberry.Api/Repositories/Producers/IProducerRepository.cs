using GoldenRaspberry.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberry.Api.Repositories.Producers
{
    public interface IProducerRepository
    {
        Task<object> GetProducersAsync(string filter, int page, int pageSize);
    }
}
