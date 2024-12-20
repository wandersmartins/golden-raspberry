using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Repositories.Studios;

namespace GoldenRaspberry.Api.Services.Studios
{
    public class StudioService : IStudioService
    {
        private readonly IStudioRepository _studioRepository;

        public StudioService(IStudioRepository studioRepository)
        {
            _studioRepository = studioRepository;
        }

        public async Task<List<Studio>> GetStudiosAsync()
        {
            return await _studioRepository.GetStudiosAsync();
        }

        public async Task<IEnumerable<object>> GetTopStudiosAsync()
        {
            return await _studioRepository.GetTopStudiosAsync();
        }
    }
}
