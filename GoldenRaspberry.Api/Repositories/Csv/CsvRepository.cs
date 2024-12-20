using CsvHelper;
using GoldenRaspberry.Api.Data;
using GoldenRaspberry.Api.Models;
using System.Globalization;

namespace GoldenRaspberry.Api.Repositories.Csv
{
    public class CsvRepository : ICsvRepository
    {
        private readonly AppDbContext _context;

        public CsvRepository(AppDbContext context)
        {
            _context = context;
        }

        public void LoadMoviesFromCsv(AppDbContext context, string filePath)
        {
            // Limpar tabelas para evitar duplicação
            ClearAllTables();

            // Cache para produtores e estúdios já processados
            var producerCache = new Dictionary<string, Producer>();
            var studioCache = new Dictionary<string, Studio>();

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1)) // Ignorar cabeçalho
            {
                var values = line.Split(';');

                // Parse dos dados
                var year = int.Parse(values[0]);
                var title = values[1];
                var isWinner = values[2].Equals("yes", StringComparison.OrdinalIgnoreCase);
                var studioNames = values[3].Split(',').Select(s => s.Trim());
                var producerNames = values[4].Split(',').Select(p => p.Trim());

                // Criar novo filme
                var movie = new Movie
                {
                    Year = year,
                    Title = title,
                    IsWinner = isWinner
                };

                // Adicionar estúdios com cache
                foreach (var studioName in studioNames)
                {
                    if (!studioCache.TryGetValue(studioName, out var studio))
                    {
                        studio = GetOrCreateStudio(context, studioName);
                        studioCache[studioName] = studio;
                    }
                    movie.MovieStudios.Add(new MovieStudio { Studio = studio });
                }

                // Adicionar produtores com cache
                foreach (var producerName in producerNames)
                {
                    if (!producerCache.TryGetValue(producerName, out var producer))
                    {
                        producer = GetOrCreateProducer(context, producerName);
                        producerCache[producerName] = producer;
                    }
                    movie.MovieProducers.Add(new MovieProducer { Producer = producer });
                }

                // Adicionar o filme ao contexto
                context.Movies.Add(movie);
            }

            // Salvar as mudanças no banco
            context.SaveChanges();
        }


        private void ClearAllTables()
        {
            // Remover todos os dados das tabelas relacionadas
            _context.MovieProducers.RemoveRange(_context.MovieProducers);
            _context.MovieStudios.RemoveRange(_context.MovieStudios);
            _context.Movies.RemoveRange(_context.Movies);
            _context.Studios.RemoveRange(_context.Studios);
            _context.Producers.RemoveRange(_context.Producers);
            _context.SaveChanges();
        }

        // Método auxiliar para buscar ou criar um estúdio
        private Studio GetOrCreateStudio(AppDbContext context, string studioName)
        {
            var studio = context.Studios.FirstOrDefault(s => s.Name == studioName);
            if (studio == null)
            {
                studio = new Studio { Name = studioName };
                context.Studios.Add(studio);
            }
            return studio;
        }

        // Método auxiliar para buscar ou criar um produtor
        private Producer GetOrCreateProducer(AppDbContext context, string producerName)
        {
            var producer = context.Producers.FirstOrDefault(p => p.Name == producerName);
            if (producer == null)
            {
                producer = new Producer { Name = producerName };
                context.Producers.Add(producer);
            }
            return producer;
        }
    }
}
