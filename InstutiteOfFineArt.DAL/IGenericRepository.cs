using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InstutiteOfFineArt.DAL
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        int Count();

        TEntity GetDetail(object id);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetById(object id);

        TEntity Find(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter);

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> filter);
    }
}
