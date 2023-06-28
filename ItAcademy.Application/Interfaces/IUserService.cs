using ItAcademy.Application.Models;
using ItAcademy.Domain;

namespace ItAcademy.Application.Interfaces;

public interface IUserService
{
    public User? GetUserByEmail(string email);
    public bool CreateUser(UserView userView);
    string LoginUser(string email, string password);
}