using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Extensions;
using BlogApp.Business.Extension_Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Business.Extention_Services.Implementations
{
	public class FileService : IFileService
	{
		public void Delete (string path, string webRootPath)
		{
			if (String.IsNullOrEmpty(path) || String.IsNullOrWhiteSpace(path)) throw new EmptyPathException();
			if (!path.StartsWith(webRootPath)) path = Path.Combine(webRootPath, path);
			if (File.Exists(path)) File.Delete(path);
		}

		public async Task SaveAsync(IFormFile file, string path, string webRootPath)
		{
			FileStream fs = new FileStream(Path.Combine(webRootPath, path), FileMode.Create);
			await file.CopyToAsync(fs);
		}

		public async Task<string> UploadAsync(IFormFile file,string path, string webRootPath,string contentType="image",int size=2)
		{
			if (!file.IsValidSize(2) || !file.IsValidType("image")) throw new InvalidFileException();
			if (String.IsNullOrEmpty(path) || String.IsNullOrWhiteSpace(path)) throw new EmptyPathException();
			string newName = RenameFile(file);
			CheckDirectory(Path.Combine(webRootPath,path));
			path = Path.Combine(path, newName);
			await SaveAsync(file, path, webRootPath);
			return path;
		}
		private void CheckDirectory(string path)
		{
			if(!Directory.Exists(path)) Directory.CreateDirectory(path);
		}
		private string RenameFile(IFormFile file) => Guid.NewGuid() + Path.GetExtension(file.FileName);
	}
}
