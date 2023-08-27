using BlogApp.DAL.Contexts;
using BlogApp.Business;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using BlogApp.Business.Services.Implements;
using BlogApp.Business.Profiles;
using BlogApp.API;
using BlogApp.Business.ExtensionServices;
using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

internal class Program
{
	private static void Main(string[] args)
	{
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
		builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
		{
			opt.User.RequireUniqueEmail = true;
			opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";
			opt.Password.RequiredLength = 8;
			opt.Password.RequireNonAlphanumeric = false;
			opt.SignIn.RequireConfirmedEmail = false;
		}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

		builder.Services.AddRepositories();
		builder.Services.AddServices();
		builder.Services.AddScoped<IEnvironmentService, EnvironmentService>();


		builder.Services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(opt =>
		{
			opt.TokenValidationParameters = new()
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,

				ValidIssuer = builder.Configuration["Jwt:Issuer"],
				ValidAudience = builder.Configuration["Jwt:Audience"],
				LifetimeValidator = (_, expires, token, _) => token != null ? DateTime.UtcNow < expires : false,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"])),

			};
		});
		builder.Services.AddAuthorization();

		builder.Services.AddAutoMapper(typeof(CategoryMappingProfile).Assembly);

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(opt =>
		{
			opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
			opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Please enter token",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "bearer"
			});

			opt.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type=ReferenceType.SecurityScheme,
							Id="Bearer"
						}
					},
					new string[]{}
				}
			});
		});

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}