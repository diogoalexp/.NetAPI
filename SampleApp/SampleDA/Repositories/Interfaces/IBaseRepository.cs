using SampleModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SampleDAL.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAsync(T contexto, Expression<Func<T, bool>> lambda, params Expression<Func<T, object>>[] joins);
        Task<T> InsertAsync(T model);
    }
}
