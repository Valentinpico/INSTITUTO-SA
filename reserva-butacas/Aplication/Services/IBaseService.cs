using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Aplication.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> SearchAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddListAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task DeleteListAsync(IEnumerable<TEntity> entities);
        Task<bool> ExistsSearchAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync();
    }
}