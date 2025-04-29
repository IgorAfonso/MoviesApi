namespace MoviesApi.Services.Interfaces;

public interface IHashService
{
    public string HashPassword(string password);
    public bool VerifyPassword(string purePassword, string userHashedPassword);
}