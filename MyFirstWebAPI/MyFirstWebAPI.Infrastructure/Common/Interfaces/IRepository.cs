using Microsoft.Data.SqlClient;
using MyFirstWebAPI.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWebAPI.Infrastructure.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Update(TEntity entity, params Expression<Func<TEntity, object>>[] updatedProperties);
        TEntity GetByFilter(Expression<Func<TEntity, bool>> filter);
        void Delete(TEntity entity);
        IEnumerable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> filter);
        void UpdateRange(IEnumerable<TEntity> entities);
        void CreateRange(IEnumerable<TEntity> entities);
        Task<List<TEntity>> READbyStoredProcedure(string sql, SqlParameter[] parameters);
        Task<int> CUDbyStoredProcedure(string sql, SqlParameter[] parameters);
    }
}
