using MoviesApi.Data;
using MoviesApi.Models;
using MoviesApi.Models.Request;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Services;

public class UserService(AppDbContext iDbContext, IHashService hashService) : IUserService
{
    private IHashService _hashService = hashService;
    public async Task<(UserModel? user, bool, string)> CreateNewUserService(CreateUserRequest user)
    {
        var userModel = new UserModel()
        {
            Id = Guid.NewGuid(),
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = _hashService.HashPassword(user.Password),
            Email = user.Email,
            IsAdmin = user.IsAdmin
        };
        
        try
        {
            var userExists = iDbContext.Users
                .Where(x => x.Username == userModel.Username)
                .Select(x => x.Id)
                .Count();
            
            var emailExists = iDbContext.Users
                .Where(x => x.Email == userModel.Email)
                .Select(x => x.Id)
                .Count();

            if (userExists != 0 || emailExists != 0)
                return (null, false, "User or email already exists");
            
            iDbContext.Users.Add(userModel);
            var result = await iDbContext.SaveChangesAsync();

            return result.Equals(0) ?
                (null, false, "Failed To Create New User") : 
                (user: userModel, true, "Success To Create New User");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (null, false, "Internal API Error To Create New User. Contact a administrator");
        }
    }
}