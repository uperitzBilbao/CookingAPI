using CookingAPI.Constantes;
using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;
using CookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Repositorio
{
    public class RecetaRepositorio : Repositorio<Receta>, IRecetaRepositorio
    {
        private readonly ILogger<RecetaRepositorio> _logger;

        public RecetaRepositorio(CookingModel context, ILogger<RecetaRepositorio> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public List<Receta> GetAllRecetasCompletas()
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_RECETAS_COMPLETAS);
                return _context.Recetas
                    .Include(r => r.RecetaIngredientes)
                    .ThenInclude(ri => ri.Ingrediente)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_RECETAS_COMPLETAS);
                throw;
            }
        }

        public Receta GetRecetaCompleta(int id)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_RECETA_ID, id);
                return _context.Recetas
                    .Include(r => r.RecetaIngredientes)
                    .ThenInclude(ri => ri.Ingrediente)
                    .FirstOrDefault(r => r.IdReceta == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_RECETA_ID, id);
                throw;
            }
        }

        public List<Receta> SearchRecetas(string? nombre, int? tipoDietaId, int? tipoAlergenoId)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.INICIANDO_BUSQUEDA_RECETAS);
                var query = _context.Recetas.AsQueryable();

                if (!string.IsNullOrEmpty(nombre))
                {
                    _logger.LogInformation(Mensajes.Logs.FILTRANDO_POR_NOMBRE, nombre);
                    query = query.Where(r => r.Nombre.Contains(nombre));
                }

                if (tipoDietaId.HasValue)
                {
                    _logger.LogInformation(Mensajes.Logs.FILTRANDO_POR_TIPO_DIETA, tipoDietaId);
                    query = query.Where(r => r.IdTipoDieta == tipoDietaId);
                }

                if (tipoAlergenoId.HasValue)
                {
                    _logger.LogInformation(Mensajes.Logs.FILTRANDO_POR_TIPO_ALERGENO, tipoAlergenoId);
                    query = query.Where(r => r.RecetaIngredientes.Any(ri => ri.Ingrediente.IngredienteAlergenos.Any(ia => ia.IdTipoAlergeno == tipoAlergenoId)));
                }

                return query.Include(r => r.RecetaIngredientes).ThenInclude(ri => ri.Ingrediente).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_BUSQUEDA_RECETAS);
                throw;
            }
        }
    }
}
