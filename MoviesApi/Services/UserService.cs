using MoviesApi.Data;
using MoviesApi.Models;
using MoviesApi.Models.Request;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Services;

public class UserService(AppDbContext iDbContext) : IUserService
{
    private readonly Dictionary<int, string> _usernameDictionary =  new Dictionary<int, string>();
    private readonly Dictionary<int, string> _emailDictionary =  new Dictionary<int, string>();
    public async Task<(UserModel? user, bool, string)> CreateNewUserService(CreateUserRequest user)
    {
        var userModel = new UserModel()
        {
            Id = Guid.NewGuid(),
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password,
            Email = user.Email,
            IsAdmin = user.IsAdmin
        };
        
        if (_usernameDictionary.ContainsValue(user.Username))
            return (null, false, "Username already exists");
        
        if (_emailDictionary.ContainsValue(user.Email))
            return (null, false, "Email already exists");
        
        try
        {
            iDbContext.Users.Add(userModel);
            var result = await iDbContext.SaveChangesAsync();

            if (result.Equals(0))
                return (null, false, "Failed To Create New User");
            
            _usernameDictionary.Add(
                _usernameDictionary.Count > 0 
                    ? _usernameDictionary.Last().Key + 1 
                    : 1,
                user.Username);
            
            _emailDictionary.Add(
                _emailDictionary.Count > 0 
                    ? _emailDictionary.Last().Key + 1
                : 1, 
                user.Email);
            
            return (user: userModel, true, "Success To Create New User");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (null, false, "Internal API Error To Create New User. Contact a administrator");
        }
    }
}