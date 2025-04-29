using MoviesApi.Models;
using MoviesApi.Models.Request;
using MoviesApi.Models.Request.UserRequests;
using MoviesApi.Models.Request.UsersRequests;

namespace MoviesApi.Services.Interfaces;

public interface IUserService
{
    public Task<(UserModel? user, bool, string)> CreateNewUserService(CreateUserRequest user);
    public Task<bool> IsSuperUser(Guid userId);
    public Task<(object?, bool, string)> UpdateUser(UpdateUserRequest? user, Guid userId);
    public Task<(object?, bool, string)> DeleteUser(Guid userId);
    public Task<(object?, bool, string)> GetUser(string? username = null);
}