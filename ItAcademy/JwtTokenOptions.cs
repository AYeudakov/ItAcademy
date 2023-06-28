#pragma warning disable CS8618
namespace ItAcademy;

public class JwtTokenOptions
{
    public JwtTokenOptions(IConfiguration configuration)
    {
        configuration.Bind(nameof(JwtTokenOptions) ,this);
    }
    
    public string Secret { get; set; }
}