namespace BlogApp.Business.Exceptions.Common;

public class WrongFileTypeException : Exception
{
	public WrongFileTypeException():base("File type is wrong.")
	{
	}

	public WrongFileTypeException(string? message) : base(message)
	{
	}
}
