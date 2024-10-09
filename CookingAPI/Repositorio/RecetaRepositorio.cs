using CookingAPI.DataModel;
using CookingAPI.Interfaces;
using CookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Repositorio
{
    public class RecetaRepositorio : Repositorio<Receta>, IRecetaRepositorio
    {

        private readonly ILogger<RecetaRepositorio> _logger;


        public RecetaRepositorio(CookingModel context, ILogger<RecetaRepositorio> logger)
            : base(context, logger)
        {
            _logger = logger;
        }

        public Receta GetRecetaCompleta(int id)
        {
            try
            {
                _logger.LogInformation($"Obteniendo receta completa con ID: {id}");
                return _context.Recetas
                    .Include(r => r.RecetaIngredientes)
                        .ThenInclude(ri => ri.Ingrediente)
                    .Include(r => r.TipoDieta)
                    .Include(r => r.TipoElaboracion)
                    .FirstOrDefault(r => r.IdReceta == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la receta completa con ID: {id}");
                throw;
            }
        }

        public List<Receta> GetAllRecetasCompletas()
        {
            try
            {
                _logger.LogInformation("Obteniendo todas las recetas completas.");
                return _context.Recetas
                    .Include(r => r.RecetaIngredientes)
                        .ThenInclude(ri => ri.Ingrediente)
                    .Include(r => r.TipoDieta)
                    .Include(r => r.TipoElaboracion)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las recetas completas.");
                throw;
            }
        }

        public List<Receta> SearchRecetas(string? nombre, int? tipoDietaId, int? tipoAlergenoId)
        {
            try
            {
                _logger.LogInformation("Iniciando búsqueda de recetas.");
                var query = _context.Recetas
                    .Include(r => r.RecetaIngredientes)
                        .ThenInclude(ri => ri.Ingrediente)
                            .ThenInclude(i => i.IngredienteAlergenos)
                                .ThenInclude(ia => ia.TipoAlergeno)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(nombre))
                {
                    _logger.LogInformation($"Filtrando recetas por nombre: {nombre}");
                    query = query.Where(r => r.Nombre.Contains(nombre));
                }

                if (tipoDietaId.HasValue)
                {
                    _logger.LogInformation($"Filtrando recetas por tipo de dieta ID: {tipoDietaId.Value}");
                    query = query.Where(r => r.IdTipoDieta == tipoDietaId.Value);
                }

                if (tipoAlergenoId.HasValue)
                {
                    _logger.LogInformation($"Filtrando recetas por tipo de alérgeno ID: {tipoAlergenoId.Value}");
                    query = query.Where(r => r.RecetaIngredientes
                        .Any(ri => ri.Ingrediente.IngredienteAlergenos
                            .Any(ia => ia.IdTipoAlergeno == tipoAlergenoId.Value)));
                }

                var result = query.ToList();
                _logger.LogInformation($"Se encontraron {result.Count} recetas.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar recetas.");
                throw;
            }
        }
    }
}
