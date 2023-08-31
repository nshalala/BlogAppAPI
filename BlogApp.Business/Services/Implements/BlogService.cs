using AutoMapper;
using BlogApp.Business.DTOs.BlogDtos;
using BlogApp.Business.DTOs.CategoryDTOs;
using BlogApp.Business.Exceptions.CategoryExceptions;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Exceptions.UserExceptions;
using BlogApp.Business.ExtensionServices.Interfaces;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp.Business.Services.Implements
{
	public class BlogService:IBlogService
	{
		private readonly IBlogRepository _blogRepo;
		private readonly ICategoryRepository _catRepo;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _contextAccessor;
		private string userId;
		private readonly UserManager<AppUser> _userManager;
		private readonly IFileService _fileService;

		public BlogService(IHttpContextAccessor contextAccessor, IBlogRepository blogRepo, IMapper mapper, ICategoryRepository catRepo, UserManager<AppUser> userManager, IFileService fileService)
		{
			_contextAccessor = contextAccessor;
			userId = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			_blogRepo = blogRepo;
			_mapper = mapper;
			_catRepo = catRepo;
			_userManager = userManager;
			_fileService = fileService;
		}

		public async Task CreateAsync(BlogCreateDto dto)
		{
			if (String.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException();
			if (!await _userManager.Users.AnyAsync(u => u.Id == userId)) throw new UserNotFoundException();
			var blog = _mapper.Map<Blog>(dto);
			blog.CoverImageUrl = await _fileService.UploadAsync(dto.CoverImage, Path.Combine("assets", "imgs", "blogs"));
			blog.AppUserId = userId;
			List<BlogCategory> blogCategories = new();
			foreach (var id in dto.CategoryIds)
			{
				var category = await _catRepo.FindByIdAsync(id);
				if (category == null) throw new CategoryNotFoundException();
				blogCategories.Add(new BlogCategory { Category = category, Blog = blog });
			}
			blog.BlogCategories = blogCategories;
			await _blogRepo.CreateAsync(blog);
			await _blogRepo.SaveAsync();
		}

		public async Task<IEnumerable<BlogListItemDto>> GetAllAsync()
		{
			//first way:

			List<BlogListItemDto> dto = new();
			var blogs = await _blogRepo.GetAll("AppUser", "BlogCategories", "BlogCategories.Category").ToListAsync();
			List<Category> categories = new();
			foreach (var blog in blogs)
			{
				categories.Clear();
				foreach (var cat in blog.BlogCategories)
				{
					categories.Add(cat.Category);
				}
				var dtoAdd = _mapper.Map<BlogListItemDto>(blog);
				dtoAdd.BlogCategories = _mapper.Map<IEnumerable<CategoryListItemDto>>(categories);
				dto.Add(dtoAdd);
			}
			return dto;
		}

		public async Task<BlogDetailDto> GetByIdAsync(int id)
		{
			if (id <= 0) throw new NegativeIdException();
			var blog = await _blogRepo.GetSingleAsync(b => b.Id == id, "AppUser", "BlogCategory", "BlogCategory.Category");
			if (blog == null) throw new NotFoundException<Blog>();
			var dto = _mapper.Map<BlogDetailDto>(blog);

			List<CategoryListItemDto> categories = new();
            foreach (var cat in blog.BlogCategories)
            {
				categories.Add(_mapper.Map<CategoryListItemDto>(await _catRepo.FindByIdAsync(cat.CategoryId)));
            }
			dto.Categories = categories;

            return dto;
		}

		public Task RemoveAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(int id, BlogUpdateDto dto)
		{
			throw new NotImplementedException();
		}
	}
}
