using AutoMapper;
using BlogApp.Business.DTOs.UserDtos;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Exceptions.UserExceptions;
using BlogApp.Business.ExtensionServices.Interfaces;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApp.Business.Services.Implements;

public class UserService : IUserService
{
	private readonly UserManager<AppUser> _userManager;
	private readonly ITokenHandler _tokenHandler;
	private readonly IMapper _mapper;

	public UserService(IMapper mapper, UserManager<AppUser> userManager, ITokenHandler tokenHandler)
	{
		_mapper = mapper;
		_userManager = userManager;
		_tokenHandler = tokenHandler;
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
	}

}
