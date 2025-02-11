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
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "{ \"Data\": [] }");
            }              
                
            string jsonContent = File.ReadAllText(path);
            var jsonData = JsonSerializer.Deserialize<RootObject>(jsonContent);

            jsonData.Data.Add(movie);

            string updatedJson = JsonSerializer.Serialize(jsonData,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, updatedJson);

            if (movie != null)
            {
                return (movie, true, "Sucesso ao Inserir o Filme");
            }
            return (null, false, "Falha ao Inserir o Filme");
        }
        public async Task<(IEnumerable<MovieModel?>, bool success, string message)> GetMovieByName(string MovieName)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "{ \"Data\": [] }");
            }

            string jsonContent = File.ReadAllText(path);
            var jsonData = JsonSerializer.Deserialize<RootObject>(jsonContent);

            var returnedValue = jsonData.Data.ToList().Where(x => string.Equals(x.Title, MovieName)).ToList();

            if (returnedValue.Count().Equals(0))
            {
                return (returnedValue, false, "Falha ao Encontrar o Filme Enviado");
            }
            return (returnedValue, true, "Filme Encontrado.");
        }
        public async Task<(IEnumerable<MovieModel?>, bool success, string message)> GetMoviesPaginated(int pageNumber, int pageSize)
        {
            string jsonContent = File.ReadAllText(path);
            var jsonData = JsonSerializer.Deserialize<RootObject>(jsonContent);

            var paginatedValues = jsonData.Data.ToList().Skip((pageNumber -1) * pageSize).Take(pageSize).ToList();

            if (paginatedValues.Count().Equals(0))
            {
                return (paginatedValues, false, "Falha ao Encontrar Dados");
            }
            return (paginatedValues, true, "Dados Encontrados.");
        }

        public async Task<(IEnumerable<MovieModel?>, bool success, string message)> UpdateMovieById(int ID, MovieModel movie)
        {
            string jsonContent = File.ReadAllText(path);
            var jsonData = JsonSerializer.Deserialize<RootObject>(jsonContent);

            var returnedValue = jsonData.Data.ToList().Where(x => x.Id == ID).ToList();

            if (returnedValue.Count() != 0)
            {
                var valuesWithoutId = returnedValue.Where(x => x.Id != ID).ToList();
                if (valuesWithoutId.Count() < returnedValue.Count())
                {
                    valuesWithoutId.Add(movie);
                    string updatedJson = JsonSerializer.Serialize(valuesWithoutId,
                        new JsonSerializerOptions { WriteIndented = true });
                    
                    File.WriteAllText(path, updatedJson);

                    return (valuesWithoutId, true, "Sucesso ao Atualizar o Filme");
                }
                return (null, false, $"Não Foi possível atualziar de ID: {ID}");
            }
            
            return (null, false, $"Falha ao Atualizar o Filme de ID: {ID}");
        }
    }
}
