using Microsoft.AspNetCore.Http;

namespace BlogApp.Business.Extension_Services.Interfaces
{
	public interface IFileService
	{
		Task<string> UploadAsync(IFormFile file, string path, string webRootPath, string contentType="image", int size=2);
		void Delete(string path, string webRootPath);
		Task SaveAsync(IFormFile file, string path, string webRootPath);
	}
}
