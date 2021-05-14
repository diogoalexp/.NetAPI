using SampleModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleDAL.Repositories.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAsync(T contexto, Expression<Func<T, bool>> lambda, params Expression<Func<T, object>>[] joins);
        Task<T> GetFirstAsync(T contexto, Expression<Func<T, bool>> lambda, params Expression<Func<T, object>>[] joins);
        Task<T> InsertAsync(T contexto);
        Task<T> UpdateAsync(T contexto);
        Task<T> DeleteAsync(T contexto);
        Task<bool> ExistsAsync(T contexto, Expression<Func<T, bool>> lambda);
    }
}
