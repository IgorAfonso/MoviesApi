using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Controllers
{
    public class MovieManipulationController : Controller
    {
        public readonly IMovieManipulationService _movieManipulationService;
        public MovieManipulationController(IMovieManipulationService movieManipulationService)
        {
            _movieManipulationService = movieManipulationService;
        }

        [HttpGet()]
        [Route("/getMovie")]
        public IActionResult GetMovieByName([FromQuery] string MovieName)
        {
            return Ok("Funcionou");
        }

        [HttpPost()]
        [Route("/postMovie")]
        public IActionResult InsertMovie([FromBody] MovieModel movie)
        {
            var result = _movieManipulationService.InsertMovieOnDb(movie);
            return Ok($"Funcionou\n {result}");
        }
    }
}
