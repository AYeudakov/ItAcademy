using System.ComponentModel.DataAnnotations;

namespace ItAcademy.Domain;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? Password { get; set; }
}