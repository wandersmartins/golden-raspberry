using GoldenRaspberry.Api.Data;
namespace GoldenRaspberry.Api.Services.Csv
{
    public interface ICsvService
    {
        void LoadMoviesFromCsv(AppDbContext context, string filePath);
    }
}
