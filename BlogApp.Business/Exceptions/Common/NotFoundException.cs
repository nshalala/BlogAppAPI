namespace BlogApp.Business.Exceptions.Common;

public class NotFoundException<T>:Exception
{
    public NotFoundException():base(typeof(T).Name + " not found.") 
    {
    }
    public NotFoundException(string message):base(message)
    {
        
    }
}
