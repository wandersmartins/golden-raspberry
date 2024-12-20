using GoldenRaspberry.Api.Models;

namespace GoldenRaspberry.Api.Repositories.Movies
{
    public interface IMovieRepository
    {
        Task<object> GetMoviesAsync(string filter, int page, int pageSize);
        Task<IEnumerable<object>> GetYearsWithMultipleWinnersAsync();
        Task<IEnumerable<int>> GetAvailableYearsAsync();
        Task<IEnumerable<object>> GetWinnersByYearAsync(int year);
    }
}
