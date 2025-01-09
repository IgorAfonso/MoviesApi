using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Controllers
{
    public class MovieManipulationController : BaseController
    {
        public readonly IMovieManipulationService _movieManipulationService;
        public MovieManipulationController(IMovieManipulationService movieManipulationService)
        {
            _movieManipulationService = movieManipulationService;
        }

        [HttpGet()]
        [Route("/getMovie")]
        public async Task<IActionResult> GetMovieByName([FromQuery] string MovieName)
        {
            try
            {
                var result = await _movieManipulationService.GetMovieByName(MovieName);

                if (!result.success)
                {
                    return CustomResponse(null, false, result.message);
                }

                return CustomResponse(result.Item1, result.success, result.message);
            }
            catch (Exception ex)
            {
                return CustomResponse(null, false, ex.Message);
            }
        }

        [HttpGet()]
        [Route("/getMovies")]
        public async Task<IActionResult> GetMovieByName([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var result = await _movieManipulationService.GetMoviesPaginated(pageNumber, pageSize);

                if (!result.success)
                {
                    return CustomResponse(null, false, result.message);
                }

                return CustomResponse(result.Item1, result.success, result.message);
            }
            catch (Exception ex)
            {
                return CustomResponse(null, false, ex.Message);
            }
        }

        [HttpPost()]
        [Route("/postMovie")]
        public async Task<IActionResult> InsertMovie([FromBody] MovieModel movie)
        {
            try
            {
                var result = await _movieManipulationService.InsertMovieOnDb(movie);
                return PostCustomResponse(result.Item1, result.success, result.message);
            }
            catch (Exception ex)
            {
                return PostCustomResponse(null, false, ex.Message);
            }
        }
    }
}
