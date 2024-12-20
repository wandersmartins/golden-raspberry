using GoldenRaspberry.Api.Models;

namespace GoldenRaspberry.Api.Repositories.MovieStudios
{
    public interface IMovieStudioRepository
    {
        Task<List<MovieStudio>> GetMovieStudiosAsync();
    }
}
