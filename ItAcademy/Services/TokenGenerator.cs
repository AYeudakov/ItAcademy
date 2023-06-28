using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ItAcademy.Services;

public class TokenGenerator : ITokenGenerator
{
    private readonly JwtTokenOptions _jwtTokenOptions;

    public TokenGenerator(IConfiguration configuration)
    {
        _jwtTokenOptions = new JwtTokenOptions(configuration);
    }

    public string? GenerateToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenKey = Encoding.UTF8.GetBytes(_jwtTokenOptions.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, email)                    
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}