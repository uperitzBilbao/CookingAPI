using CookingAPI.Constantes;
using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Repositorio
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        protected readonly CookingModel _context;
        protected readonly ILogger _logger;

        public Repositorio(CookingModel context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        // Marcar este método como virtual para permitir sobrescritura en clases derivadas
        public virtual TEntity Get(int id)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENIENDO_ENTIDAD, typeof(TEntity).Name, id);
                return _context.Set<TEntity>().Find(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_OBTENER_ENTIDAD, typeof(TEntity).Name, id);
                throw;
            }
        }

        // Marcar este método como virtual para permitir sobrescritura en clases derivadas
        public virtual IEnumerable<TEntity> GetAll()
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENIENDO_TODAS, typeof(TEntity).Name);
                return _context.Set<TEntity>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_OBTENER_TODAS, typeof(TEntity).Name);
                throw;
            }
        }

        public void Add(TEntity entity)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.AGREGANDO_ENTIDAD, typeof(TEntity).Name);
                _context.Set<TEntity>().Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_AGREGAR_ENTIDAD, typeof(TEntity).Name);
                throw;
            }
        }

        public void Update(int id, TEntity entity)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.ACTUALIZANDO_ENTIDAD, typeof(TEntity).Name, id);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_ACTUALIZAR_ENTIDAD, typeof(TEntity).Name, id);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var entity = _context.Set<TEntity>().Find(id);
                if (entity == null)
                {
                    _logger.LogWarning(Mensajes.Logs.ENTIDAD_NO_ENCONTRADA, typeof(TEntity).Name, id);
                    return;
                }

                _logger.LogInformation(Mensajes.Logs.ELIMINANDO_ENTIDAD, typeof(TEntity).Name, id);
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_ELIMINAR_ENTIDAD, typeof(TEntity).Name, id);
                throw;
            }
        }
    }
}
