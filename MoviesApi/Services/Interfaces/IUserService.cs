using MoviesApi.Models;
using MoviesApi.Models.Request;

namespace MoviesApi.Services.Interfaces;

public interface IUserService
{
    public Task<(UserModel? user, bool, string)> CreateNewUserService(CreateUserRequest user);
}