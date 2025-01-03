using HandMade.DataAccess.Data;
using HandMade.Entities.Repo_Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.DataAccess.Repo_Implementations
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public GenericRepo(ApplicationDbContext context)
        {
            _context = context;
            _dbSet= context.Set<T>();

        }
        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, string? include = null)
        {
            IQueryable<T> query= _dbSet;
            if(predicate != null)
            {
                query = query.Where(predicate);
            }
            if(include != null)
            {
                foreach (var word in include.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(word);
                }
            }
            return query.ToList();
        }

        public T GetOne(Expression<Func<T, bool>>? predicate = null, string? include = null)
        {
            IQueryable<T> query=_dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                foreach (var word in include.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(word);
                }
            }
            
            return query.SingleOrDefault();
        }
       
        public void Remove(T item)
        {
           _dbSet.Remove(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            _dbSet.RemoveRange(items);
        }
    }
}
