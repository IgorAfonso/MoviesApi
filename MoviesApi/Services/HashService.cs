using MoviesApi.Services.Interfaces;

namespace MoviesApi.Services;

public class HashService : IHashService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string purePassword, string userHashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(purePassword, userHashedPassword);
    }
}