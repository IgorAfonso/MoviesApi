namespace MoviesApi.Services.Interfaces;

public interface ITokenService
{
    public string GenerateToken(string username);
}