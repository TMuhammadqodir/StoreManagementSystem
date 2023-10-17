using Microsoft.EntityFrameworkCore;
using StoreManagementSystem.Data.DbContexts;
using StoreManagementSystem.Data.IRepositories;
using System.Linq.Expressions;
using System.Linq;
using StoreManagementSystem.Domain.Commons;

namespace StoreManagementSystem.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task AddAsync(T entity)
        => await _dbSet.AddAsync(entity);

    public void Update(T entity)
    {
        entity.UpdateAt = DateTime.UtcNow;
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
        => entity.IsDelete = true;

    public void Destroy(T entity)
        => _dbContext.Entry(entity).State = EntityState.Deleted;

    public async Task<T?> GetAsync(Expression<Func<T, bool>> expression, string[]? includes = null)
    {
        IQueryable<T> query = _dbSet;

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync(expression);
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null, bool isNoTracked = true, string[]? includes = null)
    {
        IQueryable<T> query = _dbSet;

        if (expression is not null)
            query = query.Where(expression);

        if (isNoTracked)
            query = query.AsNoTracking();

        if (includes is not null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }

    public async Task SaveAsync()
        => await _dbContext.SaveChangesAsync();
}
