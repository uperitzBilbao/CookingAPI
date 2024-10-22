using CookingAPI.Constantes;
using CookingAPI.InterfacesRepo;
using CookingAPI.InterfacesService;
using CookingAPI.Models;
using CookingAPI.Requests;

namespace CookingAPI.Services
{
    public class IngredienteService : IIngredienteService
    {
        private readonly IIngredienteRepositorio _ingredienteRepositorio;
        private readonly ITipoAlergenoRepositorio _tipoAlergenoRepositorio;
        private readonly IIngredienteAlergenoRepositorio _ingredienteAlergenoRepositorio;
        private readonly ILogger<IngredienteService> _logger;

        public IngredienteService(ITipoAlergenoRepositorio tipoAlergenoRepositorio, IIngredienteAlergenoRepositorio ingredienteAlergenoRepositorio, IIngredienteRepositorio ingredienteRepositorio, ILogger<IngredienteService> logger)
        {
            _ingredienteRepositorio = ingredienteRepositorio;
            _ingredienteAlergenoRepositorio = ingredienteAlergenoRepositorio;
            _tipoAlergenoRepositorio = tipoAlergenoRepositorio;
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
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_INGREDIENTES);
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
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_INGREDIENTE_ID, id);
                throw;
            }
        }

        public void Add(IngredienteRequest request)
        {
            try
            {
                var nuevo = new Ingrediente
                {
                    Nombre = request.Nombre,
                    IdIngrediente = request.IdTipoIngrediente,
                };

                _logger.LogInformation(Mensajes.Logs.AÑADIR_INGREDIENTE, nuevo.Nombre);
                _ingredienteRepositorio.Add(nuevo);

                foreach (string alergeno in request.Alergenos)
                {
                    var idAlergeno = _tipoAlergenoRepositorio.GetByNombre(alergeno);
                    var nuevoIngredienteAlergeno = new IngredienteAlergeno
                    {
                        IdTipoAlergeno = _tipoAlergenoRepositorio.GetByNombre(alergeno).IdTipoAlergeno,
                        IdIngrediente = nuevo.IdIngrediente
                    };
                    _ingredienteAlergenoRepositorio.Add(nuevoIngredienteAlergeno);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_AÑADIR_INGREDIENTE, request.Nombre);
                throw;
            }
        }

        public void Update(int id, IngredienteRequest request)
        {
            try
            {

                var nuevo = new Ingrediente
                {
                    Nombre = request.Nombre,
                    IdIngrediente = request.IdTipoIngrediente,
                };

                _logger.LogInformation(Mensajes.Logs.AÑADIR_INGREDIENTE, nuevo.Nombre);
                _ingredienteRepositorio.Add(nuevo);




                _logger.LogInformation(Mensajes.Logs.ACTUALIZAR_INGREDIENTE, id);
                _ingredienteRepositorio.Update(id, nuevo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_ACTUALIZAR_INGREDIENTE, id);
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
                _logger.LogError(ex, Mensajes.Error.ERROR_ELIMINAR_INGREDIENTE, id);
                throw;
            }
        }
    }
}
