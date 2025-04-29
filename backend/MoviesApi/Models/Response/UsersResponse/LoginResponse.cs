namespace MoviesApi.Models.Response;

public class LoginResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? Username { get; set; }
    public string? Token { get; set; }
}