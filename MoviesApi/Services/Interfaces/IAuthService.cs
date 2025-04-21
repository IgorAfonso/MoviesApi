namespace MoviesApi.Services.Interfaces;

public interface IAuthService
{
    public Task<(bool, string)> LoginAsync(string username, string password);
}