﻿using BlogApp.DAL.Repositories.Implementations;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.DAL
{
	public static class ServiceRegistration
	{
		public static void AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IBlogRepository, BlogRepository>();
		}
	}
}
