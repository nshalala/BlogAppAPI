using BlogApp.Business.DTOs.CategoryDTOs;
using BlogApp.Core.Entities;

namespace BlogApp.Business.Services.Interfaces;

public interface ICategoryService
{
	Task<IEnumerable<Category>> GetAllAsync();
	Task<Category> GetByIdAsync(int id);
	Task CreateAsync(CategoryCreateDto dto, string webRootPath);
}
