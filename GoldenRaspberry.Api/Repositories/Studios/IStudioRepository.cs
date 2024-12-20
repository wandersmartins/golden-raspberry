using GoldenRaspberry.Api.Models;

namespace GoldenRaspberry.Api.Repositories.Studios
{
    public interface IStudioRepository
    {
        Task<List<Studio>> GetStudiosAsync();
        Task<IEnumerable<object>> GetTopStudiosAsync();
    }
}
