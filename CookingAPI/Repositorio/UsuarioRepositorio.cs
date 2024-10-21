using CookingAPI.Constantes;
using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;
using CookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly CookingModel _context;
        private readonly ILogger<UsuarioRepositorio> _logger;

        public UsuarioRepositorio(CookingModel context, ILogger<UsuarioRepositorio> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Usuario? GetByUsername(string username)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.BUSCANDO_USUARIO, username);
                return _context.Usuarios
                    .Include(u => u.UsuarioRecetas)
                    .FirstOrDefault(u => u.Username == username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_BUSCAR_USUARIO, username);
                throw;
            }
        }

        public bool ValidateCredentials(string username, string password)
        {
            try
            {
                var usuario = GetByUsername(username);
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

        public void Create(Usuario usuario)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.CREANDO_USUARIO, usuario.Username);
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_CREAR_USUARIO, usuario.Username);
                throw;
            }
        }

        public void AsociarRecetaAUsuario(UsuarioReceta usuarioReceta)
        {
            try
            {
                _logger.LogInformation(Mensajes.Logs.ASOCIAR_RECETA_USUARIO, usuarioReceta.RecetaId, usuarioReceta.UsuarioId);
                _context.UsuarioRecetas.Add(usuarioReceta);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_ASOCIAR_RECETA_USUARIO, usuarioReceta.RecetaId, usuarioReceta.UsuarioId);
                throw;
            }
        }
    }
}
