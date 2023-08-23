namespace BlogApp.Business.Exceptions.Common
{
	public class EmptyPathException : Exception
	{
		public EmptyPathException():base("Path cannot be empty or white space.")
		{
		}

		public EmptyPathException(string? message) : base(message)
		{
		}
	}
}
