using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BlogApp.DAL.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
	public AppDbContext(DbContextOptions options) : base(options) { }
	public DbSet<Category> Categories { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
	}
}
