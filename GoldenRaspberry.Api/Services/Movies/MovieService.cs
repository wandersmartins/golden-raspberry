using GoldenRaspberry.Api.Repositories.Movies;

namespace GoldenRaspberry.Api.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<object> GetMoviesAsync(string filter, int page, int pageSize)
        {
            return await _movieRepository.GetMoviesAsync(filter, page,pageSize);
        }
        public async Task<IEnumerable<int>> GetAvailableYearsAsync()
        {
            return await _movieRepository.GetAvailableYearsAsync();
        }

        public async Task<IEnumerable<object>> GetWinnersByYearAsync(int year)
        {
            return await _movieRepository.GetWinnersByYearAsync(year);
        }

        public async Task<IEnumerable<object>> GetYearsWithMultipleWinnersAsync()
        {
            return await _movieRepository.GetYearsWithMultipleWinnersAsync();
        }
    }

}
