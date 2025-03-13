using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<MovieModel> Movies { get; set; }
    public DbSet<UserModel> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=1234;Database=SBO_Movies");
        base.OnConfiguring(optionsBuilder);
    }
}