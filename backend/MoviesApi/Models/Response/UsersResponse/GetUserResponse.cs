namespace MoviesApi.Models.Response.UsersResponse;

public class GetUserResponse
{
    public required Guid Id { get; set; }
    public required string? Username { get; set; }
    public required string? FirstName { get; set; }
    public required string? LastName { get; set; }
}