using Microsoft.AspNetCore.Http;

namespace BlogApp.Business.Extension_Services.Interfaces
{
	public interface IFileService
	{
		Task<string> UploadAsync(IFormFile file, string path, string contentType="image", int size=2);
		void Delete(string path);
		Task SaveAsync(IFormFile file, string path);
	}
}
