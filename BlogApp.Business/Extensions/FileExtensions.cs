using Microsoft.AspNetCore.Http;

namespace BlogApp.Business.Extensions
{
	public static class FileExtensions
	{
		public static bool IsValidType(this IFormFile file, string contentType) => file.ContentType.StartsWith(contentType);
		public static bool IsValidSize(this IFormFile file, int size) => file.Length <= size * 1024 * 1024;
	}
}
