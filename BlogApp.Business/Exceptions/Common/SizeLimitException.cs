namespace BlogApp.Business.Exceptions.Common
{
	public class SizeLimitException:Exception
	{
        public SizeLimitException():base("File size is too large.")
        {
            
        }
        public SizeLimitException(string message):base(message)
        {
            
        }
    }
}
