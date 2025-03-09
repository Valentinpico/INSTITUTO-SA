using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using reserva_butacas.Infrastructure.Persistence.Repositories;
using reserva_butacas.Infrastructure.Persistence.Repositories.Billboard;

namespace reserva_butacas.Aplication.Services
{
    public abstract class BaseService<TEntity>(IBaseRepository<TEntity> repository) : IBaseService<TEntity> where TEntity : class
    {

        private readonly IBaseRepository<TEntity> _repository = repository;

        public Task AddAsync(TEntity entity)
        {
            return _repository.AddAsync(entity);
        }

        public Task AddListAsync(IEnumerable<TEntity> entities)
        {
            return _repository.AddListAsync(entities);
        }

        public Task<int> CountAsync()
        {
            return _repository.CountAsync();
        }

        public Task DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public Task DeleteListAsync(IEnumerable<TEntity> entities)
        {
            return _repository.DeleteListAsync(entities);
        }

        public Task<bool> ExistsSearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.ExistsSearchAsync(predicate);
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<TEntity?> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.SearchAsync(predicate);
        }

        public Task UpdateAsync(TEntity entity)
        {
            return _repository.UpdateAsync(entity);
        }
    }
}