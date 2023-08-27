using BlogApp.Business.DTOs.UserDtos;
using BlogApp.Core.Entities;

namespace BlogApp.Business.Services.Interfaces;

public interface IUserService
{
	Task RegisterAsync(RegisterDto dto);
	Task<ResponseTokenDto> LoginAsync(LoginDto dto);
	Task<List<UserListItemDto>> GetAllAsync();
	Task<UserDetailDto> GetByNameAsync(string userName);
	Task ChangeRoleAsync(string userName, string role);
}
