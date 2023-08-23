namespace BlogApp.Business.Exceptions.CategoryExceptions
{
	public class InvalidNameException:Exception
	{
        public InvalidNameException():base("Category name cannot be null or empty.") { }

		public InvalidNameException(string? message) : base(message) { }
	}
}
