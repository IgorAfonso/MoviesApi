using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models.Request.UserRequests;
using MoviesApi.Models.Request.UsersRequests;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Controllers;

[Route("api/v1/user")]
[ApiController]
public class UserController(IUserService userService) : BaseController
{
    private IUserService _userService = userService;

    [Authorize]
    [HttpGet("{username}")]
    public async Task<IActionResult> GetUserByUsername(string? username)
    {
        try
        {
            var userReturn = await _userService.GetUser(username);
            return PostCustomResponse(userReturn.Item1, userReturn.Item2, userReturn.Item3);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new {
                message = "Internal server error",
            });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateNewUser([FromBody] CreateUserRequest user)
    {
        var insertService = await _userService.CreateNewUserService(user);
        return PostCustomResponse(insertService.user,  insertService.Item2, insertService.Item3);
    }

    [Authorize]
    [HttpPatch()]
    public async Task<IActionResult> UpdateUserInformation(
        [FromQuery] Guid idUser,
        [FromBody] UpdateUserRequest userUpdateRequest)
    {
        try
        {
            var isSuperUser = await _userService.IsSuperUser(idUser);

            if (isSuperUser)
                return PostCustomResponse(
                    null,
                    false,
                    "To update super user data it is not possible to use this endpoint.");

            var updateUserAsync = await _userService.UpdateUser(userUpdateRequest, idUser);
            return PostCustomResponse(updateUserAsync.Item1, updateUserAsync.Item2, updateUserAsync.Item3);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new {
                message = "Internal server error",
            });
        }
    }
    
    [Authorize]
    [HttpDelete()]
    public async Task<IActionResult> DeleteUser(
        [FromQuery] Guid idUser)
    {
        try
        {
            var isSuperUser = await _userService.IsSuperUser(idUser);

            if (isSuperUser)
                return PostCustomResponse(
                    null,
                    false,
                    "To delete super user data it is not possible to use this endpoint.");

            var updateUserAsync = await _userService.DeleteUser(idUser);
            return PostCustomResponse(updateUserAsync.Item1, updateUserAsync.Item2, updateUserAsync.Item3);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new {
                message = "Internal server error",
            });
        }
    }
}