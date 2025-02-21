using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Models;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Services
{
    public class MovieManipulationService(AppDbContext iDbContext) : IMovieManipulationService
    {
        private string path = "./Repository/TempDataBase.json";
        public async Task<(MovieModel?, bool success, string message)> InsertMovieOnDb(MovieModel movie)
        {
            await iDbContext.AddAsync(movie);
            await iDbContext.SaveChangesAsync();
            return (movie, true, "Success to Insert your Movie");
        }
        public async Task<(List<MovieModel?>, bool success, string message)> GetMovieByName(string? movieName)
        {
            if (movieName is null)
                return (null, false, "Insert a Movie Name")!;
            
            var dbData = await iDbContext.Movies.Where(movie => movie.Title!.Contains(movieName)).ToListAsync(); 
            return (dbData.Count == 0 ? (null, false, "Movie not found")! : (dbData, true, "Success to Find Movie"))!;
        }
        public async Task<(IEnumerable<MovieModel?>, bool success, string message)> GetMoviesPaginated(int pageNumber, int pageSize)
        {
            var paginatedValues = await iDbContext.Movies.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return paginatedValues.Count == 0 ? (paginatedValues, false, "Failed to Found Movies") : (paginatedValues, true, "Movies Founded");
        }

        public async Task<(IEnumerable<MovieModel?>, bool success, string message)> UpdateMovieById(int ID, MovieModel movie)
        {
            string jsonContent = File.ReadAllText(path);
            var jsonData = JsonSerializer.Deserialize<RootObject>(jsonContent);

            var returnedValue = jsonData.Data.ToList().Where(x => x.Id == ID).ToList();

            if(returnedValue.Count != 0)
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
