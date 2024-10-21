using CookingAPI.Constantes;
using CookingAPI.InterfacesRepo;
using CookingAPI.InterfacesService;
using CookingAPI.Models;

namespace CookingAPI.Services
{
    public class IngredienteService : IIngredienteService
    {
        private readonly IIngredienteRepositorio _ingredienteRepositorio;
        private readonly ILogger<IngredienteService> _logger;

        public IngredienteService(IIngredienteRepositorio ingredienteRepositorio, ILogger<IngredienteService> logger)
        {
            _ingredienteRepositorio = ingredienteRepositorio;
            _logger = logger;
        }

        public IEnumerable<Ingrediente> GetAll()
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_INGREDIENTES);
                return _ingredienteRepositorio.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_OBTENER_INGREDIENTES);
                throw;
            }
        }

        public Ingrediente? Get(int id)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_INGREDIENTE_ID, id);
                return _ingredienteRepositorio.Get(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_OBTENER_INGREDIENTE_ID, id);
                throw;
            }
        }

        public void Add(Ingrediente ingrediente)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.AÑADIR_INGREDIENTE, ingrediente.Nombre);
                _ingredienteRepositorio.Add(ingrediente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_AÑADIR_INGREDIENTE, ingrediente.Nombre);
                throw;
            }
        }

        public void Update(int id, Ingrediente updatedIngrediente)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.ACTUALIZAR_INGREDIENTE, id);
                _ingredienteRepositorio.Update(id, updatedIngrediente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_ACTUALIZAR_INGREDIENTE, id);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.ELIMINAR_INGREDIENTE, id);
                _ingredienteRepositorio.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_ELIMINAR_INGREDIENTE, id);
                throw;
            }
        }
    }
}
