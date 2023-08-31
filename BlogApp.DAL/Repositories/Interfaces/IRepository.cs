using BlogApp.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogApp.DAL.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity, new()
{
	public DbSet<TEntity> Table { get; }
	public IQueryable<TEntity> GetAll(params string[] includes);
	public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression);
	public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression, params string[] includes);
	public Task<TEntity> FindByIdAsync(int id);
	public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression); // niye expression, id yox?
	public Task CreateAsync(TEntity entity);
	public void Delete(TEntity entity);
	public Task SaveAsync(); 
	
}
