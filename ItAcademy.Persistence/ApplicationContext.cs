using ItAcademy.Application.Interfaces;
using ItAcademy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
#pragma warning disable CS8618

namespace ItAcademy.Persistence;

public class ApplicationContext : DbContext, IApplicationContext
{
    private readonly IConfiguration _configuration;

    public ApplicationContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("WebApiDatabase"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }

    public DbSet<User> Users { get; set; }
}