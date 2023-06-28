namespace ItAcademy.Services;

public interface ITokenGenerator
{
    public string? GenerateToken(string email);
}