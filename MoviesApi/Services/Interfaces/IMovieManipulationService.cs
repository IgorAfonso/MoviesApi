using MoviesApi.Models;

namespace MoviesApi.Services.Interfaces
{
    public interface IMovieManipulationService
    {
        public Task<(MovieModel?, bool success, string message)> InsertMovieOnDb(MovieModel movie);
        public Task<(IEnumerable<MovieModel?>, bool success, string message)> GetMovieByName(string MovieName);
        public Task<(IEnumerable<MovieModel?>, bool success, string message)> GetMoviesPaginated(int pageNumber, int pageSize);
    }
}
