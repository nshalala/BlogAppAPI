using BlogApp.Business.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Business.DTOs.CategoryDTOs
{
	public record CategoryCreateDto
	{
		public string Name { get; set; }
		public IFormFile Logo { get; set; }
		public IEnumerable<int> ids { get; set; }
	}
	public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
	{
		public CategoryCreateDtoValidator()
		{
			RuleFor(c => c.Name)
				.NotNull().WithMessage("Category name cannot be null")
				.NotEmpty().WithMessage("Category name cannot be empty")
				.MaximumLength(64).WithMessage("Category name cannot be longer than 64.");
			RuleFor(c => c.Logo)
				.NotNull().WithMessage("Category image cannot be null")
				.NotEmpty().WithMessage("Category image cannot be empty")
				.Must(l => l.IsValidType("image"))
				.Must(l => l.IsValidSize(2));
		}
	}
}
