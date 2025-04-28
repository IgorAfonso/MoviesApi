using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models.Request.UserRequests;
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

    [Authorize]
    [HttpPatch()]
    public async Task<IActionResult> UpdateUserInformation([FromQuery] Guid idUser, [FromBody] UpdateUserRequest userUpdateRequest)
    {
        var isSuperUser = await _userService.IsSuperUser(idUser);

        return isSuperUser ? 
            PostCustomResponse(null, true, "Successfully updated user") :
            PostCustomResponse(null, false, "Failed to update user");
    }
}