using CookingAPI.Constantes;
using CookingAPI.InterfacesRepo;
using CookingAPI.InterfacesService;
using CookingAPI.Models;

namespace CookingAPI.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly IRecetaRepositorio _recetaRepositorio;
        private readonly ILogger<RecetaService> _logger;
        private readonly IUsuarioRecetaRepositorio _usuarioRecetaRepositorio;
        private readonly IRecetaIngredienteRepositorio _recetaIngredienteRepositorio;
        private readonly IUserIdService _userIdService;

        public RecetaService(IRecetaRepositorio recetaRepositorio, IRecetaIngredienteRepositorio recetaIngredienteRepositorio, IUsuarioRecetaRepositorio usuarioRecetaRepositorio, IUserIdService userIdService, ILogger<RecetaService> logger)
        {
            _recetaRepositorio = recetaRepositorio;
            _logger = logger;
            _userIdService = userIdService;
            _usuarioRecetaRepositorio = usuarioRecetaRepositorio;
            _recetaIngredienteRepositorio = recetaIngredienteRepositorio;
        }

        public IEnumerable<Receta> GetAll()
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_RECETAS);
                return _recetaRepositorio.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_RECETAS);
                throw;
            }
        }

        public Receta? Get(int id)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_RECETA_ID, id);
                return _recetaRepositorio.Get(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_RECETA_ID, id);
                throw;
            }
        }

        public IEnumerable<Receta> GetAllCompleto()
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_RECETAS_COMPLETAS);
                return _recetaRepositorio.GetAllRecetasCompletas();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_RECETAS_COMPLETAS);
                throw;
            }
        }

        public Receta? GetCompleto(int id)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_RECETA_COMPLETA_ID, id);
                return _recetaRepositorio.GetRecetaCompleta(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_RECETA_COMPLETA_ID, id);
                throw;
            }
        }

        public void Add(Receta receta)
        {
            try
            {

                _logger.LogInformation(Mensajes.Logs.AÑADIR_RECETA, receta.Nombre);
                _usuarioRecetaRepositorio.CrearReceta(receta);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_AÑADIR_RECETA, receta.Nombre);
                throw;
            }
        }

        public void Update(int id, Receta updatedReceta)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.ACTUALIZAR_RECETA, id);
                _recetaRepositorio.Update(id, updatedReceta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_ACTUALIZAR_RECETA, id);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.ELIMINAR_RECETA, id);
                _recetaRepositorio.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_ELIMINAR_RECETA, id);
                throw;
            }
        }

        public IEnumerable<Receta> SearchReceta(string? nombre, int? tipoDietaId, int? tipoAlergenoId)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.BUSQUEDA_RECETA, nombre, tipoDietaId, tipoAlergenoId);
                return _recetaRepositorio.SearchRecetas(nombre, tipoDietaId, tipoAlergenoId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_BUSQUEDA_RECETAS);
                throw;
            }
        }
    }
}
