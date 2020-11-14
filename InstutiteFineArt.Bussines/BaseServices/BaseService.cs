using InstutiteOfFineArt.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InstutiteFineArt.Bussines.BaseServices
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repository;

        protected InstutiteFineArtDbContext _context;

        public BaseService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
            _context = new InstutiteFineArtDbContext();
        }
        public int Count()
        {
            return _repository.Count();
        }

        public int Create(TEntity entity)
        {
            _repository.Add(entity);
            return _context.SaveChanges();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> filter)
        {
            return _repository.Find(filter);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter)
        {
            return _repository.FindAll(filter);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public TEntity GetById(object id)
        {
            return _repository.GetById(id);
        }

        public bool Update(TEntity entity)
        {
            _repository.Update(entity);
            return _context.SaveChanges() > 0;
        }
    }
}
