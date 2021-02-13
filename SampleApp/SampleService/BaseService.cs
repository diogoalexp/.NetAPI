using SampleDAL.Repositories.Interfaces;
using SampleModel.Entities;
using SampleService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SampleService
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
            return await _repository.GetAsync(Context, m => true, joins);
        }
    }
}
