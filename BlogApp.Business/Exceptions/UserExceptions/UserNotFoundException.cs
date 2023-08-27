namespace BlogApp.Business.Exceptions.UserExceptions;

public class UserNotFoundException:Exception
{
    public UserNotFoundException():base("User does not exist.")
    {
        
    }

	public UserNotFoundException(string? message) : base(message)
	{
	}
}
