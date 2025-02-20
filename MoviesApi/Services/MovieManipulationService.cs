using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Models;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Services
{
    public class MovieManipulationService : IMovieManipulationService
    {
        private AppDbContext _iDbContext;
        private string path = "./Repository/TempDataBase.json";

        public MovieManipulationService(AppDbContext iDbContext)
        {
            _iDbContext = iDbContext;
        }

        public async Task<(MovieModel?, bool success, string message)> InsertMovieOnDb(MovieModel movie)
        {
            await _iDbContext.AddAsync(movie);
            await _iDbContext.SaveChangesAsync();
            return (movie, true, "Success to Insert your Movie");
        }
        public async Task<(List<MovieModel?>, bool success, string message)> GetMovieByName(string MovieName)
        {
            var dbData = await _iDbContext.Movies.AnyAsync(movie => movie.Title == MovieName); 
            var returnData = new List<MovieModel>();
            
            if(returnData.Count == 0)
                return (null, false, "Movie not found");
            
            return (returnData, true, "Filme Encontrado.");
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
