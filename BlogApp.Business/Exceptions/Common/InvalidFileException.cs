namespace BlogApp.Business.Exceptions.Common
{
	public class InvalidFileException : Exception
	{
		public InvalidFileException():base("File type or size is not valid.")
		{
		}

		public InvalidFileException(string? message) : base(message)
		{
		}
	}
}
