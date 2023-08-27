using BlogApp.Business.DTOs.UserDtos;
using BlogApp.Business.ExtensionServices.Interfaces;
using BlogApp.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.ExtensionServices.Implementations;

public class TokenHandler : ITokenHandler
{

	private readonly IConfiguration _configuration;

	public TokenHandler(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public ResponseTokenDto CreateToken(AppUser user, int expires = 60)
	{
		List<Claim> claims = new List<Claim>()
		{
			new Claim(ClaimTypes.Name,user.UserName),
			new Claim(ClaimTypes.GivenName, user.Name),
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Surname, user.Surname),
			new Claim(ClaimTypes.Email,user.Email)
		};
		SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));
		SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
		JwtSecurityToken jwt = new JwtSecurityToken(
			_configuration["Jwt:Issuer"],
			_configuration["Jwt:Audience"],
			claims,
			DateTime.UtcNow,
			DateTime.UtcNow.AddMinutes(expires),
			credentials);
		JwtSecurityTokenHandler jwtSecurityToken = new();
		var token = jwtSecurityToken.WriteToken(jwt);
		return new()
		{
			Token = token,
			UserName = user.UserName,
			Expires = jwt.ValidTo,
		};
	}
}
