using Microsoft.EntityFrameworkCore;
using WebAppDemo.Models.Base;

namespace WebAppDemo.DataAccess.Base;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> SelectAll();
    Task<T> SelectById(int id);
    Task Insert(T entity);
    Task Delete(int id);
}

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly DbContext Context;
        
    public Repository(DbContext context)
    {
        Context = context;
    }

    public async virtual Task Insert(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
    }

    public async virtual Task Delete(int id)
    {
        var entity = await SelectById(id);
        if (entity != null)
            Context.Set<T>().Remove(entity);
    }

    public async virtual Task<T> SelectById(int id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public async virtual Task<IEnumerable<T>> SelectAll()
    {
        return await Context.Set<T>().ToListAsync();
    }
}
