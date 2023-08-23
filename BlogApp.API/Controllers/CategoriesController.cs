using Microsoft.AspNetCore.Mvc;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Exceptions.CategoryExceptions;
using BlogApp.Business.DTOs.CategoryDTOs;

namespace BlogApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		private readonly IWebHostEnvironment _env;

		public CategoriesController(ICategoryService categoryService, IWebHostEnvironment env)
		{
			_categoryService = categoryService;
			_env = env;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _categoryService.GetAllAsync());
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				return Ok(await _categoryService.GetByIdAsync(id));
			}
			catch (NegativeIdException ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
			catch(CategoryNotFoundException ex)
			{
				return NotFound(new { Message = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message }); 
			}
		}
		[HttpPost]
		public async Task<IActionResult> Post([FromForm]CategoryCreateDto dto)
		{
			await _categoryService.CreateAsync(dto);
			return NoContent();
		}
	}
}
