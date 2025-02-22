using Azure;
using FinalProject.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Data;
using FinalProject.DAL.Repository.Interface;
namespace FinalProject.DAL.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly CustomDBContext _context;

		public GenericRepository(CustomDBContext context)
		{
			_context = context;
		}

		public async Task AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
		}

		public async Task<IQueryable<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression, int count, int page, params string[] includes)
		{
			var query = _context.Set<T>().Where(expression);
			if (count != 0 && page != 0)
			{
				query = query.Skip((page - 1) * count).Take(count);
			}
			if (includes is not null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			return query;
		}

		public async Task<T> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression, params string[] includes)
		{
			var query = _context.Set<T>().Where(expression);
			if (includes is not null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			return await query.FirstOrDefaultAsync();
		}

		public async Task<bool> isExsist(System.Linq.Expressions.Expression<Func<T, bool>> expression)
		{
			return _context.Set<T>().Where(expression).Count() > 0;
		}

		public int Save()
		{
			return _context.SaveChanges();
		}

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.Contains("String or binary data would be truncated"))
                {
                    throw new Exception("Error: The data you are trying to save is too long for one or more database columns.");
                }
                throw;
            }
        }


        public async Task Update(T entity)
		{
			_context.Set<T>().Update(entity);
		}
	}
}
