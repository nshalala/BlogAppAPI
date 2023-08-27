using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Business.DTOs.UserDtos;

public record RegisterDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
public class ResgiterDtoValidator : AbstractValidator<RegisterDto>
{
	public ResgiterDtoValidator()
	{
        RuleFor(u => u.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(32);
        RuleFor(u => u.Surname)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(32);
        RuleFor(u => u.UserName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(40)
            .Matches(@"[\w.]+");
        RuleFor(u => u.Email)
            .EmailAddress();
        RuleFor(u => u.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8)
            .Equal(u => u.ConfirmPassword);
	}
}
