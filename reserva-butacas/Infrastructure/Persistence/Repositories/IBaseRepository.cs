using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Infrastructure.Persistence.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> SearchAsync(Func<TEntity, bool> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }

}