using ItAcademy.Application.Interfaces;
using ItAcademy.Application.Models;
using ItAcademy.Domain;

namespace ItAcademy.Application.Services;

public class UserService : IUserService
{
    private readonly IDummyDbContext _context;

    public UserService(IDummyDbContext context)
    {
        _context = context;
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(user => user.Email == email);
    }
    public bool CreateUser(UserView userView)
    {
        if (GetUserByEmail(userView.Email) != null)
        {
            return false;
        }

        // TODO: validate email
        _context.Users.Add(new User
        {
            Email = userView.Email,
            Password = userView.Password
        });

        return true;
    }

    public string LoginUser(string email, string password)
    {
        throw new NotImplementedException();
    }
}