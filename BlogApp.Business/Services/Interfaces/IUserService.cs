using BlogApp.Business.DTOs.UserDtos;

namespace BlogApp.Business.Services.Interfaces;

public interface IUserService
{
	Task RegisterAsync(RegisterDto dto);
	Task<ResponseTokenDto> LoginAsync(LoginDto dto);
}
