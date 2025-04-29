using Microsoft.EntityFrameworkCore;
using MoviesApi.Config;
using MoviesApi.Models;

namespace MoviesApi.Data;

public class AppDbContext : DbContext
{
    private readonly string? _host = Configuration.GetSectionValue("DbConnection", "Host");
    private readonly string? _userName = Configuration.GetSectionValue("DbConnection", "UserName");
    private readonly string? _password = Configuration.GetSectionValue("DbConnection", "Password");
    private readonly string? _dataBase = Configuration.GetSectionValue("DbConnection", "DataBase");
    private readonly string _connectionString = "Host={0};Username={1};Password={2};Database={3}";

    private string GetConnectionString()
    {
        return string.Format(_connectionString, _host, _userName, _password, _dataBase);
    }
    
    public DbSet<MovieModel> Movies { get; set; }
    public DbSet<UserModel> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(GetConnectionString());
        base.OnConfiguring(optionsBuilder);
    }
}