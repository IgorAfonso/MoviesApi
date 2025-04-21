using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Services;

public class AuthService(AppDbContext iDbContext, IHashService hashService, ITokenService tokenService) : IAuthService
{
    private AppDbContext _iDbContext = iDbContext;
    private IHashService _hashService = hashService;
    private ITokenService _tokenService = tokenService;

    public new async Task<(bool, string)> LoginAsync(string username, string password)
    {
        try
        {
            var userPassword = 
                await _iDbContext.Users
                    .Where(x => x.Username == username)
                    .Select(x => x.Password)
                    .FirstOrDefaultAsync();

            if (userPassword is { Length: 0 }) 
                return (false, string.Empty);
            
            if(!BCrypt.Net.BCrypt.Verify(password, userPassword))
                return (false, string.Empty);
            
            var token = _tokenService.GenerateToken(username);
            return (true, token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return (false, e.Message);
        }
    }
}