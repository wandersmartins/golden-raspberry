using Microsoft.EntityFrameworkCore;
using GoldenRaspberry.Api.Data;
using GoldenRaspberry.Api.Repositories.Movies;
using GoldenRaspberry.Api.Repositories.Producers;
using GoldenRaspberry.Api.Services.Movies;
using GoldenRaspberry.Api.Services.Producers;
using GoldenRaspberry.Api.Services.MovieProducers;
using GoldenRaspberry.Api.Services.MovieStudios;
using GoldenRaspberry.Api.Services.Studios;
using GoldenRaspberry.Api.Services.Csv;
using GoldenRaspberry.Api.Repositories.MovieProducers;
using GoldenRaspberry.Api.Repositories.MovieStudios;
using GoldenRaspberry.Api.Repositories.Studios;
using GoldenRaspberry.Api.Repositories.Csv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Create in-memory database
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("MoviesDB"));

// Register repositories
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
builder.Services.AddScoped<IMovieProducerRepository, MovieProducerRepository>();
builder.Services.AddScoped<IMovieStudioRepository, MovieStudioRepository>();
builder.Services.AddScoped<IStudioRepository, StudioRepository>();
builder.Services.AddScoped<ICsvRepository, CsvRepository>();
// Register services
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IProducerService, ProducerService>();
builder.Services.AddScoped<IMovieProducerService, MovieProducerService>();
builder.Services.AddScoped<IMovieStudioService, MovieStudioService>();
builder.Services.AddScoped<IStudioService, StudioService>();

// Register CSV service
builder.Services.AddTransient<ICsvService,CsvService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin() // Permite qualquer origem
              .AllowAnyMethod() // Permite qualquer método HTTP (GET, POST, etc.)
              .AllowAnyHeader(); // Permite qualquer cabeçalho
    });
});


var app = builder.Build();

app.UseCors("AllowAllOrigins"); // Ative a política de CORS aqui

// Load CSV data into the in-memory database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var loader = scope.ServiceProvider.GetRequiredService<ICsvService>();
    string filePath = "wwwroot/data/movies.csv";  // Path to your CSV file
    loader.LoadMoviesFromCsv(context, filePath);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
