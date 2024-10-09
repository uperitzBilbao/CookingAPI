using CookingAPI.DataModel;
using CookingAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Repositorio
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        protected readonly CookingModel _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly ILogger<Repositorio<TEntity>> _logger;

        // Constructor que acepta un logger
        public Repositorio(CookingModel context, ILogger<Repositorio<TEntity>> logger)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _logger = logger;
        }

        // Métodos CRUD
        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                _logger.LogInformation($"Obteniendo todas las entidades de tipo {typeof(TEntity).Name}.");
                return _dbSet.AsNoTracking().ToList(); // Usamos AsNoTracking para optimizar rendimiento en lectura
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener todas las entidades de tipo {typeof(TEntity).Name}.");
                throw;
            }
        }

        public TEntity Get(int id)
        {
            try
            {
                _logger.LogInformation($"Obteniendo entidad de tipo {typeof(TEntity).Name} con ID: {id}");
                return _dbSet.Find(id); // No se usa AsNoTracking aquí ya que Find maneja el seguimiento
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la entidad de tipo {typeof(TEntity).Name} con ID: {id}");
                throw;
            }
        }

        public void Add(TEntity entity)
        {
            try
            {
                _logger.LogInformation($"Agregando una nueva entidad de tipo {typeof(TEntity).Name}");
                _dbSet.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al agregar una nueva entidad de tipo {typeof(TEntity).Name}");
                throw;
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                _logger.LogInformation($"Actualizando entidad de tipo {typeof(TEntity).Name}");
                _dbSet.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar entidad de tipo {typeof(TEntity).Name}");
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando entidad de tipo {typeof(TEntity).Name} con ID: {id}");
                var entity = _dbSet.Find(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    _context.SaveChanges();
                }
                else
                {
                    _logger.LogWarning($"Entidad de tipo {typeof(TEntity).Name} con ID: {id} no encontrada para eliminar.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la entidad de tipo {typeof(TEntity).Name} con ID: {id}");
                throw;
            }
        }
    }
}
