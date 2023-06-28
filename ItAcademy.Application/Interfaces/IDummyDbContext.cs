using ItAcademy.Domain;

namespace ItAcademy.Application.Interfaces;

public interface IDummyDbContext
{
    public List<User> Users { get; set; }
}