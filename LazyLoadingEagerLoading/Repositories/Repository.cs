using LazyLoadingEagerLoading.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LazyLoadingEagerLoading.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LibraryContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(LibraryContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }
    }
}
