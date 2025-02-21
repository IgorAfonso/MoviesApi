using MoviesApi.Models;

namespace MoviesApi.Services.Interfaces
{
    public interface IMovieManipulationService
    {
        public Task<(MovieModel?, bool success, string message)> InsertMovieOnDb(MovieModel movie);
        public Task<(List<MovieModel?>, bool success, string message)> GetMovieByName(string? movieName);
        public Task<(IEnumerable<MovieModel?>, bool success, string message)> GetMoviesPaginated(int pageNumber,
            int pageSize);

        public Task<(IEnumerable<MovieModel?>, bool success, string message)> UpdateMovieById(int ID,
            MovieModel movie);
    }
}