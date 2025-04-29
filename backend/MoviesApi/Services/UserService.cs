using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Models;
using MoviesApi.Models.Request;
using MoviesApi.Models.Request.UserRequests;
using MoviesApi.Models.Request.UsersRequests;
using MoviesApi.Models.Response.UsersResponse;
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

    public async Task<(object?, bool, string)> UpdateUser(UpdateUserRequest? user, Guid userId)
    {
        if (user == null || userId == Guid.Empty)
            return (null, false, "UserId or Body is null");
        
        var userDbAsync = await iDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (userDbAsync == null) 
            return (null, false, "User or email not exists");

        userDbAsync.Username = user.Username;
        userDbAsync.FirstName = user.FirstName;
        userDbAsync.LastName = user.LastName;
        userDbAsync.Email = user.Email;
                
        var result = await iDbContext.SaveChangesAsync();

        return result != 0 ? 
            (new UpdateUserResponse(){Id = userId, Username = user.Username}, true, "Success To Update User") 
            : (null, false, "Failed To Update User");
    }
    
    public async Task<(object?, bool, string)> DeleteUser(Guid userId)
    {
        if (userId == Guid.Empty)
            return (null, false, "UserId is null");
        
        var userDbAsync = await iDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (userDbAsync == null) 
            return (null, false, "User or email not exists");
        
        iDbContext.Users.Remove(userDbAsync);
        var result = await iDbContext.SaveChangesAsync();

        return result != 0 ? 
            (new DeleteUserResponse(){Id = userId}, true, "Success To Delete User") 
            : (null, false, "Failed To Delete User");
    }

    public async Task<bool> IsSuperUser(Guid userId)
    {
        var result = await iDbContext.Users
            .Where(x => x.Id == userId)
            .Select(x => x.IsAdmin)
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<(object?, bool, string)> GetUser(string? username = null)
    {
        if(username is null or "")
            return (null, false, "Username is null or empty");
        
        var userDbAsync = await iDbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        
        if(userDbAsync == null)
            return (null, false, "User or email not exists");
        
        return (new GetUserResponse()
        {
            Id = userDbAsync.Id,
            Username = userDbAsync.Username,
            FirstName = userDbAsync.FirstName,
            LastName = userDbAsync.LastName
        }, true,  "Success To Get User");
    }
}