using CookingAPI.DataModel;
using CookingAPI.Interfaces;
using CookingAPI.Models;

namespace CookingAPI.Services
{
    public class IngredienteService : IIngredienteService
    {
        private readonly CookingModel _context;
        private readonly ILogger<IngredienteService> _logger;

        public IngredienteService(CookingModel context, ILogger<IngredienteService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Ingrediente> GetAll()
        {
            _logger.LogInformation("Obteniendo todos los ingredientes.");
            return _context.Ingredientes.ToList();
        }

        public Ingrediente? Get(int id)
        {
            _logger.LogInformation($"Buscando ingrediente con ID: {id}");
            var ingrediente = _context.Ingredientes.FirstOrDefault(i => i.IdIngrediente == id);

            if (ingrediente == null)
            {
                _logger.LogWarning($"No se encontró ingrediente con ID: {id}");
            }
            return ingrediente;
        }

        public void Add(Ingrediente ingrediente)
        {
            _logger.LogInformation($"Agregando nuevo ingrediente: {ingrediente.Nombre}");
            try
            {
                _context.Ingredientes.Add(ingrediente);
                _context.SaveChanges();
                _logger.LogInformation("Ingrediente agregado con éxito.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar el ingrediente.");
                throw;
            }
        }

        public void Update(int id, Ingrediente updatedIngrediente)
        {
            _logger.LogInformation($"Actualizando ingrediente con ID: {id}");
            var ingrediente = Get(id);
            if (ingrediente != null)
            {
                try
                {
                    ingrediente.Nombre = updatedIngrediente.Nombre;
                    ingrediente.IdTipoIngrediente = updatedIngrediente.IdTipoIngrediente;

                    _context.Ingredientes.Update(ingrediente);
                    _context.SaveChanges();
                    _logger.LogInformation($"Ingrediente con ID: {id} actualizado con éxito.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al actualizar el ingrediente con ID: {id}");
                    throw;
                }
            }
            else
            {
                _logger.LogWarning($"No se encontró ingrediente con ID: {id} para actualizar.");
            }
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"Eliminando ingrediente con ID: {id}");
            var ingrediente = Get(id);
            if (ingrediente != null)
            {
                try
                {
                    _context.Ingredientes.Remove(ingrediente);
                    _context.SaveChanges();
                    _logger.LogInformation($"Ingrediente con ID: {id} eliminado con éxito.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al eliminar el ingrediente con ID: {id}");
                    throw;
                }
            }
            else
            {
                _logger.LogWarning($"No se encontró ingrediente con ID: {id} para eliminar.");
            }
        }

        // Obtener los alérgenos de un ingrediente
        public List<TipoAlergeno> GetAlergenos(int id)
        {
            _logger.LogInformation($"Obteniendo alérgenos para el ingrediente con ID: {id}");
            var ingrediente = Get(id);
            if (ingrediente != null)
            {
                return ingrediente.IngredienteAlergenos.Select(ia => ia.TipoAlergeno).ToList();
            }
            else
            {
                _logger.LogWarning($"No se encontró ingrediente con ID: {id} para obtener alérgenos.");
                return new List<TipoAlergeno>();
            }
        }
    }
}
