using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using MoviesApi.Models;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Services
{
    public class MovieManipulationService : IMovieManipulationService
    {
        private string path = "./Repository/TempDataBase.json";
        public async Task<(MovieModel?, bool success, string message)> InsertMovieOnDb(MovieModel movie)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "{ \"Data\": [] }");
                }              
                
                string jsonContent = File.ReadAllText(path);
                var jsonData = JsonSerializer.Deserialize<RootObject>(jsonContent);

                jsonData.Data.Add(movie);

                string updatedJson = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, updatedJson);

                if (movie != null)
                {
                    return (movie, true, "Sucesso ao Inserir o Filme");
                }
                return (null, false, "Falha ao Inserir o Filme");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<(IEnumerable<MovieModel?>, bool success, string message)> GetMovieByName(string MovieName)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "{ \"Data\": [] }");
                }

                string jsonContent = File.ReadAllText(path);
                var jsonData = JsonSerializer.Deserialize<RootObject>(jsonContent);

                var returnedValue = jsonData.Data.ToList().Where(x => string.Equals(x.Title, MovieName));

                if (returnedValue.Count().Equals(0))
                {
                    return (returnedValue, false, "Falha ao Encontrar o Filme Enviado");
                }
                return (returnedValue, true, "Filme Encontrado.");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<(IEnumerable<MovieModel?>, bool success, string message)> GetMoviesPaginated(int pageNumber, int pageSize)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "{ \"Data\": [] }");
                }

                string jsonContent = File.ReadAllText(path);
                var jsonData = JsonSerializer.Deserialize<RootObject>(jsonContent);

                var paginatedValues = jsonData.Data.ToList().Skip((pageNumber -1) * pageSize).Take(pageSize);

                if (paginatedValues.Count().Equals(0))
                {
                    return (paginatedValues, false, "Falha ao Encontrar Dados");
                }
                return (paginatedValues, true, "Dados Encontrados.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
