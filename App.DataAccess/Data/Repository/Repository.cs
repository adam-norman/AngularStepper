using Microsoft.EntityFrameworkCore;
using App.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext dbContext;
        internal DbSet<T> dbSet;
        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }
        public async  Task Add(T entity)
        {
          await  dbContext.AddAsync(entity);
        }

        public async Task<T> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, 
             IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query= query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (string prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query= query.Include(prop);
                }
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await  query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query= query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (string prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task Remove(T entity)
        {
              dbSet.Remove(entity);
        }

        public async Task Remove(int id)
        {
            var entityToBeRemoved = await dbSet.FindAsync(id);
           Remove(entityToBeRemoved);
        }
    }
}
