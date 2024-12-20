using GoldenRaspberry.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberry.Api.Services.Studios
{
    public interface IStudioService
    {
        Task<List<Studio>> GetStudiosAsync();
        Task<IEnumerable<object>> GetTopStudiosAsync();

    }
}
