using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reserva_butacas.Aplication.Services
{
    public interface IBaseService<TEntity, TEntityDTO, TEntityCreateDTO> where TEntity : class
    {
        Task<IEnumerable<TEntityDTO>> GetAllAsync();
        Task<TEntityDTO> GetByIdAsync(int id);
        Task<IEnumerable<TEntityDTO>> SearchAsync(Func<TEntity, bool> predicate);
        Task AddAsync(TEntityCreateDTO entity);
        Task UpdateAsync(TEntityDTO entity);
        Task DeleteAsync(int id);
    }
}