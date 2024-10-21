using CookingAPI.Constantes;
using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;
using CookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Repositorio
{
    public class IngredienteRepositorio : Repositorio<Ingrediente>, IIngredienteRepositorio
    {
        private readonly ILogger<IngredienteRepositorio> _logger;

        public IngredienteRepositorio(CookingModel context, ILogger<IngredienteRepositorio> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public override IEnumerable<Ingrediente> GetAll()
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_INGREDIENTES);
                return _context.Ingredientes
                    .Include(i => i.IngredienteAlergenos)
                    .ThenInclude(ia => ia.TipoAlergeno)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_OBTENER_INGREDIENTES);
                throw;
            }
        }

        public override Ingrediente? Get(int id)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_INGREDIENTE_ID, id);
                return _context.Ingredientes
                    .Include(i => i.IngredienteAlergenos)
                    .ThenInclude(ia => ia.TipoAlergeno)
                    .FirstOrDefault(i => i.IdIngrediente == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_OBTENER_INGREDIENTE_ID, id);
                throw;
            }
        }
    }
}
