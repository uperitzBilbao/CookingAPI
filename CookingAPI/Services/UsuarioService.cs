namespace CookingAPI.Services
{
    using global::CookingAPI.DataModel;
    using global::CookingAPI.InterfacesRepo;
    using global::CookingAPI.Models;

    namespace CookingAPI.Services
    {
        public class UsuarioService
        {
            private readonly IUsuarioRepositorio _usuarioRepositorio;
            private readonly ILogger<UsuarioService> _logger;

            public UsuarioService(IUsuarioRepositorio usuarioRepositorio, CookingModel context, ILogger<UsuarioService> logger)
            {
                _logger = logger;
                _usuarioRepositorio = usuarioRepositorio;
            }

            public bool ValidateCredentials(string username, string password)
            {
                // Valida las credenciales usando el repositorio
                return _usuarioRepositorio.ValidateCredentials(username, password);
            }

            public Usuario GetByUsername(string username)
            {
                // Obtiene el usuario por nombre de usuario
                return _usuarioRepositorio.GetByUsername(username);
            }

            public void Create(Usuario nuevoUsuario)
            {
                // Crea un nuevo usuario
                _usuarioRepositorio.Create(nuevoUsuario);
            }
        }
    }

}
