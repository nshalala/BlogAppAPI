using BlogApp.DAL.Contexts;
using BlogApp.Business;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using BlogApp.Business.Services.Implements;
using BlogApp.Business.Profiles;
using BlogApp.API;
using BlogApp.Business.Extension_Services;
using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidation(opt =>
{
	opt.RegisterValidatorsFromAssemblyContaining<CategoryService>();
});
builder.Services.AddDbContext<AppDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddScoped<IEnvironmentService, EnvironmentService>();
builder.Services.AddAutoMapper(typeof(CategoryMappingProfile).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
