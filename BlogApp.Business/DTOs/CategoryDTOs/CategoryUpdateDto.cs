using FluentValidation;
using Microsoft.AspNetCore.Http;
using BlogApp.Business.Extensions;

namespace BlogApp.Business.DTOs.CategoryDTOs;

public record CategoryUpdateDto
{
	public string? Name { get; set; }
	public IFormFile? Logo { get; set; }
}
public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        RuleFor(c => c.Name).MaximumLength(64).WithMessage("Category name cannot be longer than 64.");
        RuleFor(c => c.Logo).Must(l => l.IsValidType("image")).Must(l => l.IsValidSize(2));
    }
}