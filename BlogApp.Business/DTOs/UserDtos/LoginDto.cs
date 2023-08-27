using FluentValidation;

namespace BlogApp.Business.DTOs.UserDtos;

public record LoginDto
{
	public string UserName { get; set; }
	public string Password { get; set; }
}
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
		RuleFor(u => u.UserName)
			.NotNull()
			.NotEmpty()
			.MinimumLength(2)
			.MaximumLength(40)
			.Matches(@"[\w.]+");
		RuleFor(u => u.Password)
			.NotNull()
			.NotEmpty()
			.MinimumLength(8);
	}
}
