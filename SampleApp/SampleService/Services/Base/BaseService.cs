using SampleDAL.Repositories.Base;
using SampleModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleService.Services.Base
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<T>> GetAsync(T Context = null, params Expression<Func<T, object>>[] joins)
        {
            return (await _repository.GetAsync(Context, m => true, joins)).ToList();
        }

        public virtual async Task<T> GetFirstAsync(T Context = null, params Expression<Func<T, object>>[] joins)
        {
            return (await _repository.GetFirstAsync(Context, m => m.Id == Context.Id, joins));
        }

        public virtual async Task<T> InsertAsync(T Context)
        {
            return (await _repository.InsertAsync(Context));
        }

        public virtual async Task<T> UpdateAsync(T Context)
        {
            return (await _repository.UpdateAsync(Context));
        }

        public virtual async Task<T> DeleteAsync(T Context)
        {
            return (await _repository.DeleteAsync(Context));
        }

        public virtual async Task<bool> ExistsAsync(T Context, Expression<Func<T, bool>> joins)
        {
            return (await _repository.ExistsAsync(Context, joins));
        }

    }
}
