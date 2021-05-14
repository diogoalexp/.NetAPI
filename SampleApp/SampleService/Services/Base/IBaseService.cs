using SampleModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleService.Services.Base
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAsync(T Context = null, params Expression<Func<T, object>>[] joins);
        Task<T> GetFirstAsync(T Context = null, params Expression<Func<T, object>>[] joins);
        Task<T> InsertAsync(T Context);
        Task<T> UpdateAsync(T Context);
        Task<T> DeleteAsync(T Context);
        Task<bool> ExistsAsync(T Context, Expression<Func<T, bool>> joins);
    }
}
