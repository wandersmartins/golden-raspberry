using GoldenRaspberry.Api.Data;

namespace GoldenRaspberry.Api.Repositories.Csv
{
    public interface ICsvRepository
    {
        void LoadMoviesFromCsv(AppDbContext context, string filePath);
    }
}
