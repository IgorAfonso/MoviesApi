using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models;

public class UserModel
{
    public required Guid Id { get; set; }
    [MaxLength(50)]
    public required string? Username { get; set; }
    [MaxLength(200)]
    public required string? FirstName { get; set; }
    [MaxLength(200)]
    public required string? LastName { get; set; }
    public required string Password { get; set; }
    [MaxLength(200)]
    public required string? Email { get; set; }
    public bool IsAdmin { get; set; }
}