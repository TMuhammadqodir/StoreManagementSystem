using Microsoft.EntityFrameworkCore;
using StoreManagementSystem.Data.DbContexts;
using StoreManagementSystem.Data.IRepositories;
using System.Linq.Expressions;
using System.Linq;
using StoreManagementSystem.Domain.Commons;

namespace StoreManagementSystem.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly AppDbContext appDbContext;
    private readonly DbSet<T> dbSet;
    public Repository()
    {
        this.appDbContext = new AppDbContext();
        dbSet = appDbContext.Set<T>();
    }

    public async Task CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        entity.UpdateAt = DateTime.UtcNow;
        appDbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        entity.IsDelete = true;
    }

    public void Destroy(T entity)
    {
        dbSet.Remove(entity);
    }

    public async Task<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null)
    {
        IQueryable<T> query = dbSet.Where(expression).AsQueryable();

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        var entity = await query.FirstOrDefaultAsync(expression);
        return entity;
    }

    public IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null, bool isNoTracked = true, string[] includes = null)
    {
        IQueryable<T> query = expression is null ? dbSet.AsQueryable() : dbSet.Where(expression).AsQueryable();

        query = isNoTracked ? query.AsNoTracking() : query;

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return query;
    }

    public async Task SaveAsync()
    {
        await appDbContext.SaveChangesAsync();
    }
}
