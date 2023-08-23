namespace BlogApp.Business.Exceptions.Common
{
	public class NegativeIdException : Exception
	{
		public NegativeIdException():base("Id cannot be negative.")
		{
		}

		public NegativeIdException(string? message) : base(message)
		{
		}
	}
}
