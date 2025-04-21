using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models.Request;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Controllers;

[Route("api/v1/user")]
[ApiController]
public class UserController(IUserService userService) : BaseController
{
    private IUserService _userService = userService;
    
    [HttpPost]
    public async Task<IActionResult> CreateNewUser([FromBody] CreateUserRequest user)
    {
        var insertService = await _userService.CreateNewUserService(user);
        return PostCustomResponse(insertService.user,  insertService.Item2, insertService.Item3);
    }
}