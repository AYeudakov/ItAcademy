using ItAcademy.Application.Models;
using ItAcademy.Domain;

namespace ItAcademy.Application.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> CreateUserAsync(UserView userView);
    Task<UserView> UpdateUserAsync(UserView userView, string currentUserEmail);
    Task<string> LoginUserAsync(string email, string password);
    Task DeleteUserAsync(string email);
}