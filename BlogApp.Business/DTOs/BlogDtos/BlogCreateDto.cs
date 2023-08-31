using BlogApp.Business.Extensions;
using BlogApp.Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Business.DTOs.BlogDtos;

public record BlogCreateDto
{
	public string Title { get; set; }
	public string Description { get; set; }
	public IFormFile CoverImage { get; set; }
	public IEnumerable<int>? CategoryIds { get; set; }
}
public class BlogCreateDtoValidator : AbstractValidator<BlogCreateDto>
{
    public BlogCreateDtoValidator()
    {
		RuleFor(b => b.Title)
			.NotNull()
			.NotEmpty()
			.MaximumLength(255);
		RuleFor(b => b.Description)
			.NotNull()
			.NotEmpty()
			.MinimumLength(50);
		RuleFor(b => b.CoverImage)
			.NotNull()
			.NotEmpty()
			.Must(ci => ci.IsValidSize(2)).WithMessage("File size cannot be grater than 2 mb.")
			.Must(ci=>ci.IsValidType("image")).WithMessage("File type should be image.");
		RuleForEach(b => b.CategoryIds)
			.NotNull()
			.NotEmpty()
			.GreaterThan(0);
		RuleFor(b => b.CategoryIds)
			.Must(ci => ci.Count() > 0)
			.WithMessage("You should select at least one category.");
    }
}
