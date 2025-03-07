using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Models;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Services
{
    public class MovieManipulationService(AppDbContext iDbContext) : IMovieManipulationService
    {
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
            
            var dbData = await iDbContext.Movies
                .Where(movie => movie.Title!
                .Contains(movieName))
                .ToListAsync();
            
            return (dbData.Count == 0 
                ? (null, false, "Movie not found")! 
                : (dbData, true, "Success to Find Movie"))!;
        }
        public async Task<(IEnumerable<MovieModel?>, bool success, string message)> GetMoviesPaginated(
            int pageNumber,
            int pageSize)
        {
            var paginatedValues = await iDbContext.Movies
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return paginatedValues.Count == 0 
                ? (paginatedValues, false, "Failed to Found Movies") 
                : (paginatedValues, true, "Movies Founded");
        }

        public async Task<(MovieModel?, bool success, string message)> DeleteMovieById(int id)
        {
            var dbObject = await iDbContext.Movies.FindAsync(id);

            if (dbObject == null)
                return (null, false, $"Failed do update movie ID: {id}");
            
            var resultOperation = iDbContext.Remove(dbObject);
            
            if (resultOperation.State != EntityState.Deleted)
                return (null, false, $"Failed do update movie ID: {id}");
                
            await iDbContext.SaveChangesAsync();
            return (resultOperation.Entity, true, "Success To Delete Movie");
        }
        
        public async Task<(MovieModel?, bool success, string message)> UpdateMovieById(MovieModel movie)
        {
            var dbObject = await iDbContext.Movies.FindAsync(movie.Id);
            
            if (dbObject == null)
                return (null, false, $"Failed do update movie ID: {movie.Id}");
            
            dbObject.Id = movie.Id;
            dbObject.Title = movie.Title;
            dbObject.CreateDate = movie.CreateDate;
            dbObject.UpdateDate = movie.UpdateDate;
            dbObject.Description = movie.Description;
            dbObject.Duration = movie.Duration;
            dbObject.TicketValue = movie.TicketValue;
            dbObject.LastUpdate = DateTime.UtcNow;
                
            await iDbContext.SaveChangesAsync();
            return (dbObject, true, "Success To Update Movie");
        }
    }
}
