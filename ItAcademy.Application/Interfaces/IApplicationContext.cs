using ItAcademy.Domain;
using Microsoft.EntityFrameworkCore;

namespace ItAcademy.Application.Interfaces;

public interface IApplicationContext
{
    public DbSet<User> Users { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}