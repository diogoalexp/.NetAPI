using SampleModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleService.Services.Base
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<T>> GetAsync(T Context);
        Task<IEnumerable<T>> GetAsync(T Context, Expression<Func<T, bool>> lambda);
        Task<IEnumerable<T>> GetAsync(T Context, Expression<Func<T, object>>[] joins);
        Task<IEnumerable<T>> GetAsync(T Context, Expression<Func<T, bool>> lambda, params Expression<Func<T, object>>[] joins);
        Task<T> GetFirstAsync(int id);
        Task<T> GetFirstAsync(T Context, params Expression<Func<T, object>>[] joins);
        Task<T> InsertAsync(T Context);
        Task<T> UpdateAsync(T Context);
        Task<T> DeleteAsync(T Context);
        Task<bool> ExistsAsync(T Context, Expression<Func<T, bool>> joins);
    }
}
