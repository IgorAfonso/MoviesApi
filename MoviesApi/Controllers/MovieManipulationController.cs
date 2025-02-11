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
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet()]
        [Route("/getMoviesPaginated")]
        public async Task<IActionResult> GetMovieByName([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var result = await _movieManipulationService.GetMoviesPaginated(
                    pageNumber,
                    pageSize);

                if (!result.success)
                {
                    return CustomResponse(null, false, result.message);
                }

                return CustomResponse(result.Item1, result.success, result.message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
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
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch()]
        [Route("/updateMovie")]
        public async Task<IActionResult> UpdateMovie([FromQuery] int MovieId, [FromBody] MovieModel movie)
        {
            try
            {
                var result = await _movieManipulationService.UpdateMovieById(MovieId, movie);

                if (!result.success)
                {
                    return CustomResponse(null, false, result.message);
                }
                
                return CustomResponse(result.Item1, result.success, result.message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
