using GoldenRaspberry.Api.Models;

namespace GoldenRaspberry.Api.Services.MovieStudios
{
    public interface IMovieStudioService
    {
        Task<List<MovieStudio>> GetMovieStudiosAsync();
    }
}
