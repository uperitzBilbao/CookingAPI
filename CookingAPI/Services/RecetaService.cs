using CookingAPI.DataModel;
using CookingAPI.Interfaces;
using CookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly CookingModel _context;
        private readonly ILogger<RecetaService> _logger;

        // Constructor para inyectar el contexto de la base de datos y el logger
        public RecetaService(CookingModel context, ILogger<RecetaService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Obtener todas las recetas, incluyendo los ingredientes
        public List<Receta> GetAll()
        {
            _logger.LogInformation("Obteniendo todas las recetas.");
            return _context.Recetas.ToList();
        }

        // Obtener una receta por su ID
        public Receta? Get(int id)
        {
            _logger.LogInformation($"Buscando receta con ID: {id}");
            var receta = _context.Recetas.FirstOrDefault(r => r.IdReceta == id);

            if (receta == null)
            {
                _logger.LogWarning($"No se encontró receta con ID: {id}");
            }
            return receta;
        }

        // Obtener todas las recetas, incluyendo los ingredientes
        public List<Receta> GetAllCompleto()
        {
            _logger.LogInformation("Obteniendo todas las recetas con detalles completos.");
            return _context.Recetas
                .Include(r => r.TipoDieta)
                .Include(r => r.TipoElaboracion)
                .Include(r => r.RecetaIngredientes)
                    .ThenInclude(ri => ri.Ingrediente)
                    .ThenInclude(ial => ial.IngredienteAlergenos)
                    .ThenInclude(aler => aler.TipoAlergeno)
                .ToList();
        }

        public Receta? GetCompleto(int id)
        {
            _logger.LogInformation($"Buscando receta completa con ID: {id}");
            var receta = _context.Recetas
                .Include(r => r.TipoDieta)
                .Include(r => r.TipoElaboracion)
                .Include(r => r.RecetaIngredientes)
                    .ThenInclude(ri => ri.Ingrediente)
                    .ThenInclude(ial => ial.IngredienteAlergenos)
                    .ThenInclude(aler => aler.TipoAlergeno)
                .FirstOrDefault(r => r.IdReceta == id);

            if (receta == null)
            {
                _logger.LogWarning($"No se encontró receta completa con ID: {id}");
            }
            return receta;
        }

        // Agregar una nueva receta (incluyendo RecetaIngredientes)
        public void Add(Receta receta)
        {
            _logger.LogInformation($"Agregando nueva receta: {receta.Nombre}");
            try
            {
                _context.Recetas.Add(receta);
                _context.SaveChanges();
                _logger.LogInformation("Receta agregada con éxito.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar la receta.");
                throw; // Re-lanzar la excepción para manejarla externamente si es necesario
            }
        }

        // Actualizar una receta existente (incluyendo RecetaIngredientes)
        public void Update(int id, Receta updatedReceta)
        {
            _logger.LogInformation($"Actualizando receta con ID: {id}");
            var receta = Get(id);
            if (receta != null)
            {
                try
                {
                    // Actualizar propiedades de la receta
                    receta.Nombre = updatedReceta.Nombre;
                    receta.IdTipoDieta = updatedReceta.IdTipoDieta;
                    receta.Raciones = updatedReceta.Raciones;
                    receta.Elaboracion = updatedReceta.Elaboracion;
                    receta.Presentacion = updatedReceta.Presentacion;
                    receta.IdTipoElaboracion = updatedReceta.IdTipoElaboracion;
                    receta.TiempoMinutos = updatedReceta.TiempoMinutos;

                    // Limpiar y volver a añadir los ingredientes
                    receta.RecetaIngredientes.Clear();
                    receta.RecetaIngredientes.AddRange(updatedReceta.RecetaIngredientes);

                    _context.SaveChanges();
                    _logger.LogInformation($"Receta con ID: {id} actualizada con éxito.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al actualizar la receta con ID: {id}");
                    throw;
                }
            }
            else
            {
                _logger.LogWarning($"No se encontró receta con ID: {id} para actualizar.");
            }
        }

        // Eliminar una receta por su ID
        public void Delete(int id)
        {
            _logger.LogInformation($"Eliminando receta con ID: {id}");
            var receta = Get(id);
            if (receta != null)
            {
                try
                {
                    _context.Recetas.Remove(receta);
                    _context.SaveChanges();
                    _logger.LogInformation($"Receta con ID: {id} eliminada con éxito.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al eliminar la receta con ID: {id}");
                    throw;
                }
            }
            else
            {
                _logger.LogWarning($"No se encontró receta con ID: {id} para eliminar.");
            }
        }

        // Buscar recetas por nombre, tipo de dieta o alérgenos
        public List<Receta> Search(string? nombre, int? tipoDietaId, int? tipoAlergenoId)
        {
            _logger.LogInformation("Iniciando búsqueda de recetas.");
            var query = _context.Recetas.AsQueryable();

            // Incluir RecetaIngredientes e Ingredientes desde el principio
            query = query.Include(r => r.RecetaIngredientes)
                         .ThenInclude(ri => ri.Ingrediente);

            // Filtrar por nombre
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(r => r.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
                _logger.LogInformation($"Filtrando recetas por nombre: {nombre}");
            }

            // Filtrar por tipo de dieta
            if (tipoDietaId.HasValue)
            {
                query = query.Where(r => r.IdTipoDieta == tipoDietaId.Value);
                _logger.LogInformation($"Filtrando recetas por tipo de dieta con ID: {tipoDietaId.Value}");
            }

            // Filtrar por alérgenos
            if (tipoAlergenoId.HasValue)
            {
                query = query.Where(r => r.RecetaIngredientes
                    .Any(ri => ri.Ingrediente.IngredienteAlergenos
                        .Any(ia => ia.IdTipoAlergeno == tipoAlergenoId.Value)));
                _logger.LogInformation($"Filtrando recetas por tipo de alérgeno con ID: {tipoAlergenoId.Value}");
            }

            return query.ToList(); // Devolver la lista final de recetas
        }
    }
}
