using BlogApp.Business.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Business.DTOs.BlogDtos
{
	public record BlogUpdateDto
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string CoverImageUrl { get; set; }
		public IFormFile? CoverImage { get; set; }
		public IEnumerable<int>? CategoryIds { get; set; }
	}
	public class BlogUpdateDtoValidator : AbstractValidator<BlogUpdateDto>
	{
		public BlogUpdateDtoValidator()
		{
			RuleFor(b => b.Title)
				.MaximumLength(255);
			RuleFor(b => b.Description)
				.MinimumLength(50);
			RuleFor(b => b.CoverImage)
				.Must(ci => ci.IsValidSize(2))
				.Must(ci => ci.IsValidType("image"));
			RuleForEach(b => b.CategoryIds)
				.GreaterThan(0);
		}
	}
}


