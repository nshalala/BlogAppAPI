﻿using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Extensions;
using BlogApp.Business.ExtensionServices.Interfaces;
using Microsoft.AspNetCore.Http;
namespace BlogApp.Business.ExtensionServices.Implementations;

public class FileService : IFileService
{
	private readonly IEnvironmentService _env;

	public FileService(IEnvironmentService env)
	{
		_env = env;
	}

	public void Delete(string path)
	{
		if (String.IsNullOrEmpty(path) || String.IsNullOrWhiteSpace(path)) throw new EmptyPathException();
		if (!path.StartsWith(_env.webRootPath)) path = Path.Combine(_env.webRootPath, path);
		if (File.Exists(path)) File.Delete(path);
	}

	public async Task SaveAsync(IFormFile file, string path)
	{
		FileStream fs = new FileStream(Path.Combine(_env.webRootPath, path), FileMode.Create);
		await file.CopyToAsync(fs);
	}

	public async Task<string> UploadAsync(IFormFile file, string path, string contentType = "image", int size = 2)
	{
		if (!file.IsValidSize(2)) throw new SizeLimitException();
		if (!file.IsValidType("image")) throw new WrongFileTypeException();
		if (String.IsNullOrEmpty(path) || String.IsNullOrWhiteSpace(path)) throw new EmptyPathException();
		string newName = RenameFile(file);
		CheckDirectory(path);
		path = Path.Combine(path, newName);
		await SaveAsync(file, path);
		return path;
	}
	private void CheckDirectory(string path)
	{
		if (!Directory.Exists(Path.Combine(_env.webRootPath, path))) Directory.CreateDirectory(Path.Combine(_env.webRootPath, path));
	}
	private string RenameFile(IFormFile file) => Guid.NewGuid() + Path.GetExtension(file.FileName);
}
