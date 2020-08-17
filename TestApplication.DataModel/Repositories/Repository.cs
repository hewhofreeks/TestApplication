using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestApplication.DataModel;

namespace CallCenterService.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private MHContext _context;
        private DbSet<T> _dbSet;

        public Repository(MHContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public Task AddRangeAsync(IEnumerable<T> items)
        {
            return _dbSet.AddRangeAsync(items);
        }

        public void AddRange(IEnumerable<T> items)
        {
            _dbSet.AddRange(items);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> exp)
        {
            return await _dbSet.FirstOrDefaultAsync(exp);
        }


        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }


        public IQueryable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _dbSet.AsQueryable().Where(exp);
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }

    public interface IRepository<T> where T : class, new()
    {
        IQueryable<T> Where(Expression<Func<T, bool>> exp);

        Task<T> FirstOrDefault(Expression<Func<T, bool>> exp);



        Task<int> SaveChangesAsync();
        int SaveChanges();

        void Add(T item);
        Task AddRangeAsync(IEnumerable<T> items);
        void AddRange(IEnumerable<T> items);
        void Delete(T item);

        IQueryable<T> AsQueryable();
    }
}