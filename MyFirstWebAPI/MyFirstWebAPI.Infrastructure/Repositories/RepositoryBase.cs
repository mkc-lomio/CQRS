using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyFirstWebAPI.Domain;
using MyFirstWebAPI.Domain.Base;
using MyFirstWebAPI.Domain.Common.Interfaces;
using MyFirstWebAPI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity> :
             IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly MyFirstWebAPIDbContext _context;
        public RepositoryBase(MyFirstWebAPIDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public TEntity Create(TEntity entity)
        {
            return this._context.Set<TEntity>().Add(entity).Entity;
        }

        public IEnumerable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> filter)
        {
            return this._context.Set<TEntity>().Where(filter).AsNoTracking().ToList();
        }

        public TEntity GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            return this._context.Set<TEntity>().Where(filter).AsNoTracking().FirstOrDefault();
        }

        public TEntity Update(TEntity entity)
        {
            this._context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }


        public TEntity Update(TEntity entity, params Expression<Func<TEntity, object>>[] updatedProperties)
        {
            this._context.Set<TEntity>().Attach(entity);

            foreach (var property in updatedProperties)
            {
                this._context.Entry(entity).Property(property).IsModified = true;
            }

            return entity;
        }

        public void Delete(TEntity entity)
        {
            this._context.Set<TEntity>().Remove(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            this._context.Set<TEntity>().UpdateRange(entities);
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            this._context.Set<TEntity>().AddRange(entities);
        }


        public Task<List<TEntity>> READbyStoredProcedure(string sql, SqlParameter[] parameters)
        {
            return this._context.Set<TEntity>().FromSqlRaw(sql, parameters).ToListAsync();
        }

        public Task<int> CUDbyStoredProcedure(string sql, SqlParameter[] parameters)
        {
            return this._context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

    }
}
