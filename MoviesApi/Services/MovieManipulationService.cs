using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using MoviesApi.Models;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Services
{
    public class MovieManipulationService : IMovieManipulationService
    {
        public string InsertMovieOnDb(MovieModel movie)
        {
            var jsonConfig = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string json = JsonSerializer.Serialize(movie);

            File.AppendAllText("./Repository/TempDataBase.json",json);

            return movie.ToString();
        }
    }
}
