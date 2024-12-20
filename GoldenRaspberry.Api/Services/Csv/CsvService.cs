using GoldenRaspberry.Api.Data;
using GoldenRaspberry.Api.Repositories.Csv;

namespace GoldenRaspberry.Api.Services.Csv
{
    public class CsvService : ICsvService
    {
        private readonly ICsvRepository _csvRepository;

        public CsvService(ICsvRepository csvRepository)
        {
            _csvRepository = csvRepository;
        }

        public void LoadMoviesFromCsv(AppDbContext context, string filePath)
        {
            _csvRepository.LoadMoviesFromCsv(context, filePath);
        }
    }
}