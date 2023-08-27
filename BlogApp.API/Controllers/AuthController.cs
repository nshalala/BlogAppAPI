using BlogApp.Business.DTOs.UserDtos;
using BlogApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class AuthController : ControllerBase
	{
		private readonly IUserService _userService;

		public AuthController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Register([FromForm] RegisterDto dto)
		{
			await _userService.RegisterAsync(dto);
			return NoContent();
		}
		[HttpPost("[action]")]
		public async Task<IActionResult> Login([FromForm] LoginDto dto)
		{
			return Ok(await _userService.LoginAsync(dto));
		}
	}
}
