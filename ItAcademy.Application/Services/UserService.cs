using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using ItAcademy.Application.Interfaces;
using ItAcademy.Application.Models;
using ItAcademy.Domain;
using ItAcademy.Domain.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ItAcademy.Application.Services;

public class UserService : IUserService
{
    private readonly IApplicationContext _context;
    private readonly JwtTokenOptions _jwtOptions;

    public UserService(IApplicationContext context, IConfiguration configuration)
    {
        _context = context;
        _jwtOptions = new JwtTokenOptions(configuration);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
    }
    
    public async Task<bool> CreateUserAsync(UserView userView)
    {
        if (await GetUserByEmailAsync(userView.Email) != null)
        {
            return false;
        }
        
        //TODO: GetHashFor userView.Password
        
        await _context.Users.AddAsync(new User
        {
            Email = userView.Email,
            Password = userView.Password
        });

        await _context.SaveChangesAsync(CancellationToken.None);        

        return true;
    }

    public async Task<UserView> UpdateUserAsync(UserView userView, string currentUserEmail)
    {
        if (await GetUserByEmailAsync(userView.Email) != null)
        {
            throw new UserAlreadyExistException(userView.Email);
        }
        
        var user = await GetUserByEmailAsync(currentUserEmail) ?? throw new UserNotFoundException();

        user.Email = string.IsNullOrEmpty(userView.Email) ? user.Email : userView.Email;
        user.Password = string.IsNullOrEmpty(userView.Password) ? user.Password : userView.Password;

        _context.Users.Update(user);

        await _context.SaveChangesAsync(CancellationToken.None);

        return userView;
    }

    public async Task<string> LoginUserAsync(string email, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(entity => entity.Email == email && entity.Password == password);

        if (user is null)
        {
            throw new UserNotFoundException(email);
        }

        return GenerateToken(email, _jwtOptions.Secret);
    }

    public async Task DeleteUserAsync(string email)
    {
        var user = await GetUserByEmailAsync(email);

        _context.Users.Remove(user!);

        await _context.SaveChangesAsync(CancellationToken.None);
    }


    private string GenerateToken(string email, string secret)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(secret);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, email)
        };

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
            SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescription);
        
        return tokenHandler.WriteToken(token);
    }
}