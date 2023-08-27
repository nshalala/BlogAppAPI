using BlogApp.Business.ExtensionServices.Implementations;
using BlogApp.Business.ExtensionServices.Interfaces;
using BlogApp.Business.Services.Implements;
using BlogApp.Business.Services.Interfaces;
using BlogApp.DAL.Repositories.Implementations;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Business;

public static class ServiceRegistration
{
	public static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<ICategoryService, CategoryService>();
		services.AddScoped<IFileService, FileService>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<ITokenHandler, TokenHandler>();
	}
	public static void AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<ICategoryRepository, CategoryRepository>();
	}
}
