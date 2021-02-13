using Microsoft.EntityFrameworkCore;
using SampleDAL.DataAccess;
using SampleDAL.Repositories.Interfaces;
using SampleModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SampleDAL
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly T _context;
        private readonly ApplicationDbContext _db;

        private Expression<Func<T, bool>> ExpressionBase { get; set; }

        public BaseRepository(ApplicationDbContext db)
        {
            this._db = db;
            ExpressionBase = e => true;
        }

        private IQueryable<T> Get(T contexto, params Expression<Func<T, object>>[] joins)
        {

            var query = _db.Set<T>().Where(ExpressionBase);
            if (joins == null)
                return query;
            return joins.Aggregate(query, (current, include) => current.Include(include));
        }

        public virtual async Task<IEnumerable<T>> GetAsync(T contexto, Expression<Func<T, bool>> lambda, params Expression<Func<T, object>>[] joins)
        {
            if (lambda != null)
                return await Get(contexto, joins).Where(lambda).ToListAsync();
            else
                return await Get(contexto, joins).ToListAsync();
        }

        public virtual async Task<T> InsertAsync(T model)
        {
            _db.Attach(model);
            await _db.SaveChangesAsync();
            return model;
        }
    }
}
