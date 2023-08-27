namespace BlogApp.Business.Exceptions.UserExceptions;

public class LoginFailedException:Exception
{
    public LoginFailedException():base("Username or Password is wrong.")
    {
        
    }
    public LoginFailedException(string message):base(message)
    {
        
    }
}
