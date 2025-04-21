using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/movie")]
    public class MovieController(IMovieManipulationService movieManipulationService) : BaseController
    {
        private readonly IMovieManipulationService _movieManipulationService = movieManipulationService;

        [HttpGet("{name}")]
        public async Task<IActionResult> GetMovieByName(string? name)
        {
            try
            {
                var result = await _movieManipulationService.GetMovieByName(name);
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

        [HttpGet("paginated")]
        public async Task<IActionResult> GetMovieByName([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var result = await _movieManipulationService.GetMoviesPaginated(
                    pageNumber,
                    pageSize);

                return !result.success ? 
                    CustomResponse(null, false, result.message) : 
                    CustomResponse(result.Item1, result.success, result.message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost()]
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
