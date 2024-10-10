using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;
using CookingAPI.Models;

namespace CookingAPI.Repositorio
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {

        private readonly ILogger<UsuarioRepositorio> _logger;

        public UsuarioRepositorio(CookingModel context, ILogger<UsuarioRepositorio> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public Usuario? GetByUsername(string username)
        {
            try
            {
                _logger.LogInformation($"Buscando usuario con nombre de usuario: {username}");
                return _context.Usuarios.SingleOrDefault(u => u.Username == username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar el usuario por nombre de usuario");
                return null;
            }
        }

        public bool ValidateCredentials(string username, string password)
        {
            try
            {
                var usuario = GetByUsername(username);

                if (usuario == null)
                {
                    _logger.LogWarning($"Usuario {username} no encontrado.");
                    return false;
                }

                // Verificar la contraseña usando el hash almacenado
                if (!PasswordHelper.VerifyPassword(password, usuario.Password))
                {
                    _logger.LogWarning($"Contraseña incorrecta para el usuario {username}.");
                    return false;
                }

                _logger.LogInformation($"Usuario {username} autenticado correctamente.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al validar las credenciales para el usuario {username}");
                return false;
            }
        }


        public void Create(Usuario usuario)
        {
            try
            {
                usuario.Password = PasswordHelper.HashPassword(usuario.Password); // Hash de la contraseña
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                _logger.LogInformation($"Usuario {usuario.Username} creado correctamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario.");
                throw;
            }
        }

    }
}
