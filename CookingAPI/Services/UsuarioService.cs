using CookingAPI.Constantes;
using CookingAPI.InterfacesRepo;
using CookingAPI.InterfacesService;
using CookingAPI.Models;
using CookingAPI.Repositorio;

namespace CookingAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IUsuarioRecetaRepositorio _usuarioRecetaRepositorio;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(IUsuarioRepositorio usuarioRepositorio, IUsuarioRecetaRepositorio usuarioRecetaRepositorio, ILogger<UsuarioService> logger)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _logger = logger;
            _usuarioRecetaRepositorio = usuarioRecetaRepositorio;
        }

        public bool ValidateCredentials(string username, string password)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.VALIDAR_CREDENCIALES, username);
                var usuario = _usuarioRepositorio.GetByUsername(username);
                if (usuario == null)
                {
                    _logger.LogError(Mensajes.Error.USUARIO_NO_ENCONTRADO, username);
                    return false;
                }

                return PasswordHelper.VerifyPassword(password, usuario.Password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_VALIDAR_CREDENCIALES, username);
                throw;
            }
        }

        public Usuario GetByUsername(string username)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENIENDO_USUARIO, username);
                return _usuarioRepositorio.GetByUsername(username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_USUARIO, username);
                throw;
            }
        }

        public int GetUserIdByUsername(string username)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.OBTENIENDO_USERID, username);
                var usuario = _usuarioRepositorio.GetByUsername(username);
                if (usuario == null)
                {
                    throw new Exception(Mensajes.Error.USUARIO_NO_ENCONTRADO);
                }

                return usuario.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_USERID, username);
                throw;
            }
        }

        public void Create(Usuario nuevoUsuario)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.CREAR_USUARIO, nuevoUsuario.Username);
                _usuarioRepositorio.Create(nuevoUsuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_CREAR_USUARIO, nuevoUsuario.Username);
                throw;
            }
        }

        public void AsociarRecetaAUsuario(UsuarioReceta usuarioReceta)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.ASOCIAR_RECETA, usuarioReceta.RecetaId, usuarioReceta.UsuarioId);
                _usuarioRepositorio.AsociarRecetaAUsuario(usuarioReceta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_ASOCIAR_RECETA, usuarioReceta.RecetaId, usuarioReceta.UsuarioId);
                throw;
            }
        }
    }
}
