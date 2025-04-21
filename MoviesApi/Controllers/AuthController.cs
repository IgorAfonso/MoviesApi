using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models.Request;
using MoviesApi.Models.Response;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IAuthService authService) : BaseController
{
    private IAuthService _authService = authService;
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
    {
        var loginOperation = await _authService.LoginAsync(user.username ,user.password);

        if (loginOperation is { Item1: false, Item2: "" })
            return Unauthorized(new LoginResponse()
            {
                Success = false,
                Message = "User name or password is incorrect",
                Token = string.Empty
            });
        
        return !loginOperation.Item1 && loginOperation.Item2 != string.Empty ?
            StatusCode(500, new LoginResponse()
            {
                Success = false,
                Message = loginOperation.Item2,
                Token = string.Empty
            }):
            Ok(new LoginResponse()
            {
                Success = loginOperation.Item1,
                Message = "Successful Login",
                Token = loginOperation.Item2
            });
    }
}