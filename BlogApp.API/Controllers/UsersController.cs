using BlogApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _userService.GetAllAsync());
		}
		[HttpGet("{userName}")]
		public async Task<IActionResult> Get(string userName)
		{
			return Ok(await _userService.GetByNameAsync(userName));
		}
		[HttpPatch("[action]")]
		public async Task<IActionResult> ChangeRole(string userName, string role)
		{
			await _userService.ChangeRoleAsync(userName, role);
			return Ok();
		}
	}
}
