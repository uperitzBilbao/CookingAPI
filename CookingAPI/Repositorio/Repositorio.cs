using CookingAPI.DataModel;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Repositorio
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        protected readonly CookingModel _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repositorio(CookingModel context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }
    }


}
