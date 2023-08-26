using AutoMapper;
using BlogApp.Business.DTOs.CategoryDTOs;
using BlogApp.Business.Exceptions.CategoryExceptions;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Extension_Services.Interfaces;
using BlogApp.Business.Extensions;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Interfaces;

namespace BlogApp.Business.Services.Implements;

public class CategoryService : ICategoryService
{
	private readonly ICategoryRepository _repo;
	private readonly IFileService _fileService;
	private readonly IMapper _mapper;

	public CategoryService(ICategoryRepository repo, IMapper mapper, IFileService fileService)
	{
		_repo = repo;
		_mapper = mapper;
		_fileService = fileService;
	}

	public async Task CreateAsync(CategoryCreateDto dto)
	{
		if (String.IsNullOrEmpty(dto.Name) || String.IsNullOrWhiteSpace(dto.Name)) throw new InvalidNameException();
		if (!dto.Logo.IsValidSize(2)) throw new SizeLimitException();
		if (!dto.Logo.IsValidType("image")) throw new WrongFileTypeException();
		Category entity = _mapper.Map<Category>(dto);
		entity.LogoUrl = await _fileService.UploadAsync(dto.Logo, Path.Combine("assets", "imgs", "categories"));
		await _repo.CreateAsync(entity);
		await _repo.SaveAsync();
	}

	public async Task RemoveAsync(int id)
	{
		var entity = await _getCategoryAsync(id);
		_fileService.Delete(entity.LogoUrl);
		_repo.Delete(entity);
		await _repo.SaveAsync();
;	}

	public async Task<IEnumerable<CategoryListItemDto>> GetAllAsync()
	{
		return _mapper.Map<IEnumerable<CategoryListItemDto>>(_repo.GetAll());
	}

	public async Task<CategoryDetailDto> GetByIdAsync(int id)
	{
		var entity = await _getCategoryAsync(id);
		return _mapper.Map<CategoryDetailDto>(entity);
	}

	public async Task UpdateAsync(int id, CategoryUpdateDto dto)
	{
		var entity = await _getCategoryAsync(id);
		if (dto.Name != null)
		{
			_mapper.Map(dto, entity);
		}
		if(dto.Logo !=null)
		{
			_fileService.Delete(entity.LogoUrl);
			entity.LogoUrl = await _fileService.UploadAsync(dto.Logo, Path.Combine("assets", "imgs", "categories"));
		}
		await _repo.SaveAsync();
	}

	private async Task<Category> _getCategoryAsync(int id)
	{
		if (id <= 0) throw new NegativeIdException();
		var entity = await _repo.FindByIdAsync(id);
		return entity ?? throw new CategoryNotFoundException();
	}
}
