using GoldenRaspberry.Api.Models;
using GoldenRaspberry.Api.Repositories.MovieStudios;

namespace GoldenRaspberry.Api.Services.MovieStudios
{
    public class MovieStudioService : IMovieStudioService
    {
        private readonly IMovieStudioRepository _movieStudioRepository;

        public MovieStudioService(IMovieStudioRepository movieStudioRepository)
        {
            _movieStudioRepository = movieStudioRepository;
        }

        public async Task<List<MovieStudio>> GetMovieStudiosAsync()
        {
            return await _movieStudioRepository.GetMovieStudiosAsync();
        }
    }
}
