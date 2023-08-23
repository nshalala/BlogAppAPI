using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BlogApp.DAL.Contexts;

public class AppDbContext:DbContext
{
	public AppDbContext(DbContextOptions options) : base(options) { }
	public DbSet<Category> Categories { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
	}
}
