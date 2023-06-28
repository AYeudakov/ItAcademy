namespace ItAcademy.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    // ReSharper disable once UnusedMember.Global
    public UserNotFoundException() { }

    public UserNotFoundException(string email, string password) 
        : this($"User with email: {email} and password {password}") { }

    // ReSharper disable once MemberCanBePrivate.Global
    public UserNotFoundException(string message) 
        : base(message) { }

    public UserNotFoundException(string message, Exception innerException) 
        : base(message, innerException) { }
}