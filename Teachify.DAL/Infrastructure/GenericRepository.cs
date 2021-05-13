using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Teachify.DALS.Infrastructure.Interfaces;

namespace Teachify.DALS.Infrastructure
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		/// <summary>
		/// This readonly variable holds the db context created from the unit of work
		/// </summary>
		private readonly IUnitOfWork _unitOfWork;
		/// <summary>
		/// This variable initializes the dbSet
		/// </summary>
		internal DbSet<T> dbSet;

		/// <summary>
		/// This constructor initializes the unit of work and the dbset
		/// </summary>
		/// <param name="unitOfWork"></param>
		public GenericRepository(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
			this.dbSet = _unitOfWork.Db.Set<T>();
		}
		/// <summary>
		/// this method gets the single or default generic result from a db selection
		/// </summary>
		/// <param name="whereCondition"></param>
		/// <returns></returns>
		public T SingleOrDefault(Expression<Func<T, bool>> whereCondition)
		{
			var dbResult = dbSet.Where(whereCondition).FirstOrDefault();
			return dbResult;
		}

		/// <summary>
		/// This method returns a single result using the where clause
		/// </summary>
		/// <param name="whereCondition"></param>
		/// <returns></returns>
		public T Single(Expression<Func<T, bool>> whereCondition)
		{
			var dbResult = dbSet.Where(whereCondition).First();
			return dbResult;
		}

		/// <summary>
		/// This method gets all result from the database
		/// </summary>
		/// <returns></returns>
		public IEnumerable<T> GetAll()
		{
			return dbSet.AsEnumerable();
		}

		/// <summary>
		/// This method gets all using the where condition
		/// </summary>
		/// <param name="whereCondition"></param>
		/// <returns></returns>
		public IEnumerable<T> GetAll(Expression<Func<T, bool>> whereCondition)
		{
			return dbSet.Where(whereCondition).AsEnumerable();
		}

		/// <summary>
		/// This method inserts records into the database
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual T Insert(T entity)
		{
			dynamic obj = dbSet.Add(entity).Entity;
			this._unitOfWork.Db.SaveChanges();
			return obj;

		}
		/// <summary>
		/// This method gets a single record the database
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual T Get(object Id)
		{
			return dbSet.Find(Id);
		}
		/// <summary>
		/// This method gets a single record the database by Composite Keys
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual T GetByCompositeKeys(object[] Id)
		{
			return dbSet.Find(Id);
		}
        /// <summary>
        /// This method updates the database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
		{
			dbSet.Attach(entity);
			_unitOfWork.Db.Entry(entity).State = EntityState.Modified;
			this._unitOfWork.Db.SaveChanges();
		}

		/// <summary>
		/// This method updates all records given a list
		/// </summary>
		/// <param name="entities"></param>
		public virtual void UpdateAll(IList<T> entities)
		{
			foreach (var entity in entities)
			{
				dbSet.Attach(entity);
				_unitOfWork.Db.Entry(entity).State = EntityState.Modified;
			}
			this._unitOfWork.Db.SaveChanges();
		}

		/// <summary>
		/// This method deletes a record from the database
		/// </summary>
		/// <param name="whereCondition"></param>
		public void Delete(Expression<Func<T, bool>> whereCondition)
		{
			IEnumerable<T> entities = this.GetAll(whereCondition);
			foreach (T entity in entities)
			{
				if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
				{
					dbSet.Attach(entity);
				}
				dbSet.Remove(entity);
			}
			this._unitOfWork.Db.SaveChanges();
		}

		/// <summary>
		/// This method deletes by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public void DeleteById(object id)
		{
			T entity = dbSet.Find(id);
			if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
			{
				dbSet.Attach(entity);
			}
			dbSet.Remove(entity);
			this._unitOfWork.Db.SaveChanges();
		}

        /// <summary>
        /// This method deletes by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void DeleteByCompositeId(object[] id)
        {
            T entity = dbSet.Find(id);
            if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            this._unitOfWork.Db.SaveChanges();
        }

        /// <summary>
        /// This method checks if records exists from the database using the where condition
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public bool Exists(Expression<Func<T, bool>> whereCondition)
		{
			return dbSet.Any(whereCondition);
		}

		//--------------Extra generic methods--------------------------------

		public T SingleOrDefaultOrderBy(Expression<Func<T, bool>> whereCondition, Expression<Func<T, int>> orderBy, string direction)
		{
			if (direction == "ASC")
			{
				return dbSet.Where(whereCondition).OrderBy(orderBy).FirstOrDefault();

			}
			else
			{
				return dbSet.Where(whereCondition).OrderByDescending(orderBy).FirstOrDefault();
			}
		}

		public int Count(Expression<Func<T, bool>> whereCondition)
		{
			return dbSet.Where(whereCondition).Count();
		}

		public IEnumerable<T> GetPagedRecords(Expression<Func<T, bool>> whereCondition, Expression<Func<T, string>> orderBy, int pageNo, int pageSize)
		{
			return (dbSet.Where(whereCondition).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize)).AsEnumerable();
		}

		public IEnumerable<T> GetAllWithPagedRecords(Expression<Func<T, string>> orderBy, int pageNo, int pageSize)
		{
			return (dbSet.OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize)).AsEnumerable();
		}

		public async Task<int> CountAll() => await dbSet.CountAsync();

		public async Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate)
			=> await dbSet.CountAsync(predicate);

		public virtual async Task<ICollection<T>> GetAllAsyn()
		{

			return await dbSet.ToListAsync();
		}
		public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
		{
			return await dbSet.SingleOrDefaultAsync(match);
		}
		public virtual async Task<T> GetAsync(object id)
		{
			return await dbSet.FindAsync(id);
		}

		public virtual async Task<T> GetAsyncByCompositeIdAsync(object[] id)
		{
			return await dbSet.FindAsync(id);
		}


		public async Task DeleteAsync(Expression<Func<T, bool>> whereCondition)
		{
			IEnumerable<T> entities = await GetAllAsync(whereCondition);
			foreach (T entity in entities)
			{
				if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
				{
					dbSet.Attach(entity);
				}
				dbSet.Remove(entity);
			}
			await _unitOfWork.Db.SaveChangesAsync();


		}

		public async Task<bool> ExistsAsync(Expression<Func<T, bool>> whereCondition)
		{
			return await dbSet.AnyAsync(whereCondition);
		}
		public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
		  => dbSet.FirstOrDefaultAsync(predicate);


		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await dbSet.ToListAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition)
		{
			return await dbSet.Where(whereCondition).ToListAsync();
		}

		public async Task<IList<T>> GetAllEntityAsync(Expression<Func<T, bool>> whereCondition)
		{
			return await dbSet.Where(whereCondition).ToListAsync();
		}

        public async Task<int> CountAllAsync()
        {
            return await dbSet.CountAsync();
        }


        public async Task<T> GetByIdAsync(object id)
		{
			return await dbSet.FindAsync(id);
		}

		public async Task<T> InsertAsync(T entity)
		{
			await dbSet.AddAsync(entity);
			await _unitOfWork.Db.SaveChangesAsync();
			return entity;
		}

		public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> whereCondition)
		{
			var dbResult = await dbSet.Where(whereCondition).FirstAsync();
			return dbResult;
		}

		public async Task UpdateAllAsync(IList<T> entities)
		{
			foreach (var entity in entities)
			{
				dbSet.Attach(entity);
				_unitOfWork.Db.Entry(entity).State = EntityState.Modified;
			}
			await _unitOfWork.Db.SaveChangesAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			dbSet.Attach(entity);
			_unitOfWork.Db.Entry(entity).State = EntityState.Modified;
			await _unitOfWork.Db.SaveChangesAsync();
		}

		public async Task<T> GetByByCompositeIdAsync(object[] code)
		{
			return await dbSet.FindAsync(code);
		}

		public async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
		{
			return await dbSet.Where(predicate).ToListAsync();
		}

	}
}