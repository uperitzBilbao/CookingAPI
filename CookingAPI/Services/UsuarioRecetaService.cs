using CookingAPI.Constantes;
using CookingAPI.InterfacesRepo;
using CookingAPI.InterfacesService;
using CookingAPI.Models;

namespace CookingAPI.Services
{
    public class UsuarioRecetaService : IUsuarioRecetaService
    {
        private readonly IUsuarioRecetaRepositorio _usuarioRecetaRepositorio;
        private readonly ILogger<UsuarioRecetaService> _logger;

        public UsuarioRecetaService(IUsuarioRecetaRepositorio usuarioRecetaRepositorio, ILogger<UsuarioRecetaService> logger)
        {
            _usuarioRecetaRepositorio = usuarioRecetaRepositorio;
            _logger = logger;
        }

        public void CrearReceta(Receta receta)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.CREAR_RECETA, receta.Nombre);
                _usuarioRecetaRepositorio.CrearReceta(receta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_CREAR_RECETA, receta.Nombre);
                throw;
            }
        }

        public IEnumerable<Receta> ObtenerRecetasDelUsuario()
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_RECETAS_USUARIO);
                return _usuarioRecetaRepositorio.ObtenerRecetasDelUsuario();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_OBTENER_RECETAS_USUARIO);
                throw;
            }
        }

        public bool ActualizarReceta(int recetaId, Receta recetaActualizada)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.ACTUALIZAR_RECETA, recetaId);
                return _usuarioRecetaRepositorio.ActualizarReceta(recetaId, recetaActualizada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_ACTUALIZAR_RECETA, recetaId);
                throw;
            }
        }

        public bool EliminarReceta(int recetaId)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.ELIMINAR_RECETA, recetaId);
                return _usuarioRecetaRepositorio.EliminarReceta(recetaId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_ELIMINAR_RECETA, recetaId);
                throw;
            }
        }

        public Receta? ObtenerReceta(int recetaId)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENER_RECETA_ID, recetaId);
                return _usuarioRecetaRepositorio.ObtenerReceta(recetaId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_OBTENER_RECETA_ID, recetaId);
                throw;
            }
        }
    }
}
