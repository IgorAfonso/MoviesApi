using MoviesApi.Models;
using MoviesApi.Models.Request;
using MoviesApi.Models.Request.UserRequests;

namespace MoviesApi.Services.Interfaces;

public interface IUserService
{
    public Task<(UserModel? user, bool, string)> CreateNewUserService(CreateUserRequest user);
    public Task<bool> IsSuperUser(Guid userId);
}