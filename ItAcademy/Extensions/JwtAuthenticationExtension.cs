using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ItAcademy.Extensions;

public static class JwtAuthenticationExtension
{
    public static IServiceCollection RegisterJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtTokenOptions = new JwtTokenOptions(configuration);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtTokenOptions.Secret)
                    ),
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        return services;
    }
}