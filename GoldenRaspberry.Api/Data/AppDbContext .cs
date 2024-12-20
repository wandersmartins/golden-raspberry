using GoldenRaspberry.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberry.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<MovieProducer> MovieProducers { get; set; }
        public DbSet<MovieStudio> MovieStudios { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieProducer>()
                .HasOne(mp => mp.Movie)
                .WithMany(m => m.MovieProducers)
                .HasForeignKey(mp => mp.MovieId);

            modelBuilder.Entity<MovieProducer>()
                .HasOne(mp => mp.Producer)
                .WithMany(p => p.MovieProducers)
                .HasForeignKey(mp => mp.ProducerId);

            modelBuilder.Entity<MovieStudio>()
                .HasOne(ms => ms.Movie)
                .WithMany(m => m.MovieStudios)
                .HasForeignKey(ms => ms.MovieId);

            modelBuilder.Entity<MovieStudio>()
                .HasOne(ms => ms.Studio)
                .WithMany(s => s.MovieStudios)
                .HasForeignKey(ms => ms.StudioId);

            // Configuração das relações
            modelBuilder.Entity<Movie>()
                .Navigation(m => m.MovieProducers)
                .AutoInclude();

            modelBuilder.Entity<Movie>()
                .Navigation(m => m.MovieStudios)
                .AutoInclude();
        }
    }
}
