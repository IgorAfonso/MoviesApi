using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Controllers
{
    public class MovieManipulationController(IMovieManipulationService movieManipulationService) : BaseController
    {
        private readonly IMovieManipulationService _movieManipulationService = movieManipulationService;

        [HttpGet()]
        [Route("/get-movie-by-name")]
        public async Task<IActionResult> GetMovieByName([FromQuery] string? movieName)
        {
            try
            {
                var result = await _movieManipulationService.GetMovieByName(movieName);
                return !result.success 
                    ? CustomResponse(null, false, result.message) 
                    : CustomResponse(result.Item1, result.success, result.message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet()]
        [Route("/get-movies-paginated")]
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
        [Route("/create-movie")]
        public async Task<IActionResult> InsertMovie([FromBody] MovieModel movieObject)
        {
            try
            {
                var result = await _movieManipulationService.InsertMovieOnDb(movieObject);
                return PostCustomResponse(result.Item1, result.success, result.message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete()]
        [Route("/delete-movie")]
        public async Task<IActionResult> DeleteMovie([FromQuery] int movieId)
        {
            try
            {
                var result = await _movieManipulationService.DeleteMovieById(movieId);
                return !result.success 
                    ? CustomResponse(null, false, result.message) 
                    : CustomResponse(result.Item1, result.success, result.message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut()]
        [Route("/update-movie")]
        public async Task<IActionResult> DeleteMovie([FromBody] MovieModel movieObject)
        {
            try
            {
                var result = await _movieManipulationService.UpdateMovieById(movieObject);
                return !result.success ?
                    CustomResponse(null, false, result.message)
                    : CustomResponse(result.Item1, result.success, result.message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
