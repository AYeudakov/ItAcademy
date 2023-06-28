using ItAcademy.Application.Interfaces;
using ItAcademy.Domain;

namespace ItAcademy.Persistence;

public class DummyDbContext : IDummyDbContext
{
    public DummyDbContext()
    {
        Users = DbContextInitializer.Seed<User>(15);
        
        Users.Add(new User
        {
            Email = "string",
            Password = "string"
        });
    }
    
    public List<User> Users { get; set; }
    
}