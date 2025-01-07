using MoviesApi.Models;

namespace MoviesApi.Services.Interfaces
{
    public interface IMovieManipulationService
    {
        public string InsertMovieOnDb(MovieModel movie);
    }
}
