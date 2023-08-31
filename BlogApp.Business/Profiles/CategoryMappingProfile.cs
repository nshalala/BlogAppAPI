using AutoMapper;
using BlogApp.Business.DTOs.CategoryDTOs;
using BlogApp.Core.Entities;

namespace BlogApp.Business.Profiles;

public class CategoryMappingProfile:Profile
{
	public CategoryMappingProfile()
	{
		CreateMap<CategoryCreateDto, Category>();
		CreateMap<CategoryUpdateDto, Category>();
		CreateMap<Category, CategoryListItemDto>();
		CreateMap<Category, CategoryDetailDto>();
		CreateMap<BlogCategory, CategoryListItemDto>();
	}
}
