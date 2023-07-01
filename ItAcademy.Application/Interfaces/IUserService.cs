using ItAcademy.Application.Models;
using ItAcademy.Domain;

namespace ItAcademy.Application.Interfaces;

public interface IUserService
{
    public Task<User?> GetUserByEmailAsync(string email);
    public Task<bool> CreateUserAsync(UserView userView);
    public Task<string> LoginUserAsync(string email, string password);
}