using AutoMapper;
using BlogApp.Business.DTOs.BlogDtos;
using BlogApp.Core.Entities;

namespace BlogApp.Business.Profiles
{
	public class BlogMappingProfile:Profile
	{
		public BlogMappingProfile()
		{
			CreateMap<BlogCreateDto, Blog>();
			CreateMap<BlogUpdateDto, Blog>();
			CreateMap<Blog, BlogListItemDto>();
			CreateMap<Blog, BlogDetailDto>();
		}
	}
}
