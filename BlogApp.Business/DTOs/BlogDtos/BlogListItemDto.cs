using BlogApp.Business.DTOs.CategoryDTOs;

namespace BlogApp.Business.DTOs.BlogDtos
{
	public record BlogListItemDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int ViewCount { get; set; }
		public string CoverImageUrl { get; set; }
        public bool  IsDeleted { get; set; }
        public DateTime CreatedTime { get; set; }
		public AuthorDto AppUser { get; set; }
		public IEnumerable<CategoryListItemDto> BlogCategories { get; set; }
	}

}

