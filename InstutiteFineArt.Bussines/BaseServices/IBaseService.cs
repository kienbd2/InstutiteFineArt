using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InstutiteFineArt.Bussines.BaseServices
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        int Create(TEntity entity);

        bool Update(TEntity entity);

        int Count();

        TEntity GetById(object id);

        IEnumerable<TEntity> GetAll();

        TEntity Find(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter);
    }
}
