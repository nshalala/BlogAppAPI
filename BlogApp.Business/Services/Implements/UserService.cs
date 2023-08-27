using AutoMapper;
using BlogApp.Business.DTOs.UserDtos;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Exceptions.UserExceptions;
using BlogApp.Business.ExtensionServices.Interfaces;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BlogApp.Business.Services.Implements;

public class UserService : IUserService
{
	private readonly UserManager<AppUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly ITokenHandler _tokenHandler;
	private readonly IMapper _mapper;

	public UserService(IMapper mapper, UserManager<AppUser> userManager, ITokenHandler tokenHandler, RoleManager<IdentityRole> roleManager)
	{
		_mapper = mapper;
		_userManager = userManager;
		_tokenHandler = tokenHandler;
		_roleManager = roleManager;
	}

	public async Task ChangeRoleAsync(string userName, string role)
	{
		var user = await _userManager.FindByNameAsync(userName);
		if (user == null) throw new UserNotFoundException();

		var r = await _roleManager.Roles.AnyAsync(r => r.Name == role);
		if (!r) throw new RoleNotFoundException();

		var currentRoles = await _userManager.GetRolesAsync(user);
		if (!currentRoles.Contains("Admin"))
		{
			await _userManager.RemoveFromRolesAsync(user, currentRoles);
			await _userManager.AddToRoleAsync(user, role);
		}else
		{
			throw new Exception("You can not change role of admin");
		}

	}

	public async Task<List<UserListItemDto>> GetAllAsync()
	{
		var users = await _userManager.Users.ToListAsync();
		var dto = _mapper.Map<List<UserListItemDto>>(users);
        foreach (var item in dto)
        {
			var roles = await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(item.UserName));
			item.Role = roles[0];
        }
        return dto;
	}

	public async Task<UserDetailDto> GetByNameAsync(string userName)
	{
		var user = await _userManager.FindByNameAsync(userName);
		if (user == null) throw new UserNotFoundException();
		var dto = _mapper.Map<UserDetailDto>(user);
		var roles = await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(dto.UserName));
		dto.Role = roles[0];
		return dto;
	}

	public async Task<ResponseTokenDto> LoginAsync(LoginDto dto)
	{
		var user = await _userManager.FindByNameAsync(dto.UserName);
		if (user == null) throw new LoginFailedException();
		var result = await _userManager.CheckPasswordAsync(user, dto.Password);
		if (!result) throw new LoginFailedException();
		return _tokenHandler.CreateToken(user);
	}

	public async Task RegisterAsync(RegisterDto dto)
	{
		var user = _mapper.Map<AppUser>(dto);
		if (await _userManager.Users.AnyAsync(u => u.Name == dto.Name || u.Email == dto.Email)) 
			throw new AlreadyExistsException("Username or Email is taken.");
		var result = await _userManager.CreateAsync(user,dto.Password);
		if (!result.Succeeded)
		{
			StringBuilder sb = new StringBuilder();
            foreach (var error in result.Errors)
            {
				sb.Append(error.Description + " ");
            }
			throw new RegisterFailedException(sb.ToString().TrimEnd());
        }
		var res = await _userManager.AddToRoleAsync(user, "member");
	}


}
