using Microsoft.EntityFrameworkCore;
using System.Linq;
using WCT.Infrastructure.DBContexts;

namespace WCT.Infrastructure.Repositories
{
    internal abstract class BaseRepository<T> where T : class
    {
        private readonly DBContext _context;

        public BaseRepository(DBContext context)
        {
            this._context = context;
        }

        protected T Create(T entity)
        {
            return this._context.Set<T>().Add(entity).Entity;
        }

        protected IQueryable<T> Entity(bool trackChanges)
        {
            if (!trackChanges)
                return this._context.Set<T>().AsNoTracking();

            return this._context.Set<T>();
        }

        protected void Delete(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }
    }
}