using Microsoft.Extensions.Configuration;

#pragma warning disable CS8618
namespace ItAcademy.Application;

public class JwtTokenOptions
{
    public JwtTokenOptions(IConfiguration configuration)
    {
        configuration.Bind(nameof(JwtTokenOptions) ,this);
    }
    
    public string Secret { get; set; }
}