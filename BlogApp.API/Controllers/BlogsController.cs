using BlogApp.Business.DTOs.BlogDtos;
using BlogApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BlogsController : ControllerBase
{
	private readonly IBlogService _blogService;

	public BlogsController(IBlogService blogService)
	{
		_blogService = blogService;
	}

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		return Ok(await _blogService.GetAllAsync());
	}
	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		return Ok(await _blogService.GetByIdAsync(id));
	}
	[HttpPost]
	public async Task<IActionResult> Post([FromForm]BlogCreateDto dto)
	{
		try
		{
			await _blogService.CreateAsync(dto);
			return NoContent();
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}
