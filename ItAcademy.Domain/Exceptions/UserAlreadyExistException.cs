namespace ItAcademy.Domain.Exceptions;

public class UserAlreadyExistException : Exception
{
    public UserAlreadyExistException() { }
    
    public UserAlreadyExistException(string email) 
        : base($"User with email: {email} already exist") { }
    
    public UserAlreadyExistException(string email, Exception? innerException) 
        : base(email, innerException) { }
}