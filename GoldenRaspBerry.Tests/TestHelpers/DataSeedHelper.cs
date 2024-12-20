using GoldenRaspberry.Api.Data;
using GoldenRaspberry.Api.Models;

namespace GoldenRaspberry.Tests.TestHelpers
{
    public static class DataSeedHelper
    {
        public static void SeedMovies(AppDbContext context)
        {
            context.MovieProducers.RemoveRange(context.MovieProducers);
            context.MovieStudios.RemoveRange(context.MovieStudios);
            context.Movies.RemoveRange(context.Movies);
            context.Producers.RemoveRange(context.Producers);
            context.Studios.RemoveRange(context.Studios);
            context.SaveChanges();

            var producers = new List<Producer>
            {
                new Producer { Id = 1, Name = "Producer A" },
                new Producer { Id = 2, Name = "Producer B" }
            };

            var studios = new List<Studio>
            {
                new Studio { Id = 1, Name = "Studio A" },
                new Studio { Id = 2, Name = "Studio B" }
            };

            context.Producers.AddRange(producers);
            context.Studios.AddRange(studios);
            context.SaveChanges();

            context.Movies.AddRange(new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Title = "Movie A",
                    Year = 2020,
                    IsWinner = false,
                    MovieProducers = new List<MovieProducer>
                    {
                        new MovieProducer { ProducerId = 1 },
                        new MovieProducer { ProducerId = 2 }
                    },
                    MovieStudios = new List<MovieStudio>
                    {
                        new MovieStudio { StudioId = 1 }
                    }
                },
                new Movie
                {
                    Id = 2,
                    Title = "Movie B",
                    Year = 2021,
                    IsWinner = true,
                    MovieProducers = new List<MovieProducer>
                    {
                        new MovieProducer { ProducerId = 2 }
                    },
                    MovieStudios = new List<MovieStudio>
                    {
                        new MovieStudio { StudioId = 2 }
                    }
                }
            });

            context.SaveChanges();
        }
    }
}
