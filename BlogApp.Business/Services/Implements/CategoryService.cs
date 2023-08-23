using BlogApp.Business.DTOs.CategoryDTOs;
using BlogApp.Business.Exceptions.CategoryExceptions;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Extension_Services.Interfaces;
using BlogApp.Business.Extensions;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Business.Services.Implements;

public class CategoryService : ICategoryService
{
	public readonly ICategoryRepository _repo;
	public readonly IFileService _fileService;

	public CategoryService(ICategoryRepository repo, IFileService fileService)
	{
		_repo = repo;
		_fileService = fileService;
	}

	public async Task CreateAsync(CategoryCreateDto dto, string webRootPath)
	{
		if (String.IsNullOrEmpty(dto.Name) || String.IsNullOrWhiteSpace(dto.Name)) throw new InvalidNameException();
		if (dto.Logo.IsValidSize(2) || dto.Logo.IsValidType("image")) throw new InvalidFileException();
		Category entity = new Category()
		{
			Name = dto.Name,
			LogoUrl = await _fileService.UploadAsync(dto.Logo, Path.Combine("assets","imgs","Categories"),webRootPath),
			IsDeleted = false,
		};
		await _repo.CreateAsync(entity);
		await _repo.SaveAsync();
	}

	public async Task<IEnumerable<Category>> GetAllAsync()
	{
		return await _repo.GetAll().ToListAsync();
	}

	public async Task<Category> GetByIdAsync(int id)
	{
		if (id <= 0) throw new NegativeIdException();
		var entity = await _repo.FindByIdAsync(id);
		return entity ?? throw new CategoryNotFoundException();
	}
}
