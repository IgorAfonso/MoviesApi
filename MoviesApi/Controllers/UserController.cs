using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Models.Request;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Controllers;

public class UserController(IUserService userService) : BaseController
{
    private IUserService _userService = userService;

    [HttpPost]
    [Route("/create-user")]
    public async Task<IActionResult> CreateNewUser([FromBody] CreateUserRequest user)
    {
        var insertService = await _userService.CreateNewUserService(user);
        
        if (insertService.Item2)
            return Ok(insertService);
        
        return BadRequest(insertService);
    }
}