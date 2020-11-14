using InstutiteOfFineArt.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq.Expressions;

namespace InstutiteOfFineArt.DAL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private InstutiteFineArtDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public GenericRepository()
        {
            _context = new InstutiteFineArtDbContext();
            _dbSet = _context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public virtual TEntity GetDetail(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual int Count()
        {
            return _dbSet.Count();
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.AddOrUpdate(entity);
            _context.SaveChanges();
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.FirstOrDefault(filter);
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Where(filter).ToList();
        }


        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return orderBy != null ? orderBy(query) : query;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }
    }
}
