﻿using BackEnd.Core.Domain;
using BackEnd.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.EF.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly StudentDbContext _db;
        protected DbSet<T> Entity= null;
       

       

        public GenericRepo(StudentDbContext _db )
        {
            this._db = _db;
            Entity = _db.Set<T>();
           
        }

        #region main operations
        public void Add(T entity)
        {
            Entity.Add(entity);
        }
        public async Task AddAsync(T entity)
        {
              await Entity.AddAsync(entity);
        }

        public void AddRange(List<T> entities)
        {
            Entity.AddRange(entities);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            Entity.UpdateRange(entities);
        }
        public void Update(T entity)
        {
            var x = Entity.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            Entity.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            Entity.RemoveRange(entities);
        }

        #endregion

        #region GetALL Async =>(IQueryable)
        public IQueryable<T> GetAll()
        {
            return Entity.AsQueryable();
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var result = Entity.Where(i => true);


            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return result;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return Entity.Where(whereCondition);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
        {
            var result = Entity.Where(whereCondition);
            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);
            return result;
        }
        public async Task<IEnumerable<T>> GetAllAsync(string includeProperties = null)
        {
            IQueryable<T> query = Entity;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }

            return await query.ToListAsync();
        }

        #endregion

        #region GetALL Async =>(IQueryable)
        public async Task<double> Count()
        {
            return await Entity.CountAsync();
        }

        public async Task<double> Count(params Expression<Func<T, object>>[] includes)
        {
            var result = Entity.Where(i => true);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.CountAsync();
        }

        public async Task<double> Count(Expression<Func<T, bool>> whereCondition)
        {
            return await Entity.Where(whereCondition).CountAsync();
        }

        public async Task<double> Count(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
        {
            var result = Entity.Where(whereCondition);
            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);
            return await result.CountAsync(); ;
        }

        #endregion

        #region GetALL Async =>(IEnumerable)

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Entity.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var result = Entity.Where(i => true);
            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);
            return await result.ToListAsync(); ;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await Entity.Where(whereCondition).ToListAsync();

        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
        {
            var result = Entity.Where(whereCondition);
            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);
            return await result.ToListAsync();
        }

        #endregion

        #region GetById Async
        
        public async Task<T> GetByAppUserIdAsync(string appUserId,
        params Expression<Func<T, object>>[] includes)
        {
            // Create expression: x => x.AppUserId == appUserId
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, "AppUserId");
            var constant = Expression.Constant(appUserId);
            var equal = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equal, parameter);

            IQueryable<T> query = Entity;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(lambda);
        }
    


        public async Task<T> GetByIdAsync(string id)
        {
            return await Entity.FindAsync(id);

        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Entity.FindAsync(id);

        }
        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
        {
            var result = Entity.Where(whereCondition);
            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);
            return await result.FirstOrDefaultAsync();

        }


        #endregion
        #region  SingleOrDefaultAsync
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Entity.AnyAsync(predicate);
        }
        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await Entity.Where(whereCondition).FirstOrDefaultAsync();
        }
        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
        {
            var result = Entity.Where(whereCondition);
            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.FirstOrDefaultAsync();
        }
        public async Task<T> SingleOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await Entity.Where(whereCondition).AsNoTracking().FirstOrDefaultAsync();
        }
       
       public async Task<T> SingleOrDefaultAsNoTrackingAsync(Expression<Func<T, bool>> whereCondition, params Expression<Func<T, object>>[] includes)
        {
            var result = Entity.Where(whereCondition);
            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T,bool>> predicat) { 
              return await Entity.FirstOrDefaultAsync(predicat);
        }
        public async Task<T> GetByIdWithIncludeAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Entity;
            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(filter);
        }
        #endregion

        #region OrderBy
        public IEnumerable<T> OrderBy(Expression<Func<T, bool>> whereCondition)
        {
            return Entity.OrderBy(whereCondition);
        }
        public IEnumerable<T> OrderByDescending(Expression<Func<T, bool>> whereCondition)
        {
            return Entity.OrderByDescending(whereCondition);
        }
        #endregion


        #region SaveAllAsync
        public async Task<bool> SaveAllAsync()
        {


            return await _db.SaveChangesAsync() > 0;

        }


        #endregion
        #region checkState
        public bool checkState(T entity, string state)
        {
            var x = _db.Entry(entity).State;
            return (_db.Entry(entity).State.ToString().ToLower() == state.ToLower().Trim()) ? true : false;
        }

        public DbSet<T> GetContext()
        {
            return Entity;

        }

        public async Task<T> FirstAsync()
        {
            return await Entity.FirstOrDefaultAsync();
        }
        public async Task<T> FirstAsNoTrackingAsync()
        {
            return await Entity.AsNoTracking().FirstOrDefaultAsync();
        }



        #endregion

    }
}
