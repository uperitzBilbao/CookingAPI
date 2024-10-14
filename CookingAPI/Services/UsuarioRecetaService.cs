namespace CookingAPI.Services
{
    using global::CookingAPI.DataModel;
    using global::CookingAPI.InterfacesRepo;
    using global::CookingAPI.Models;

    namespace CookingAPI.Services
    {
        public class UsuarioRecetaService
        {
            private readonly IUsuarioRecetaRepositorio _usuarioRecetaRepositorio;
            private readonly ILogger<UsuarioRecetaService> _logger;

            public UsuarioRecetaService(IUsuarioRecetaRepositorio usuarioRecetaRepositorio, CookingModel context, ILogger<UsuarioRecetaService> logger)
            {
                _logger = logger;
                _usuarioRecetaRepositorio = usuarioRecetaRepositorio;
            }

            public bool ValidateCredentials(string username, string password)
            {
                // Valida las credenciales usando el repositorio
                return _usuarioRecetaRepositorio.ValidateCredentials(username, password);
            }

            public Usuario GetByUsername(string username)
            {
                // Obtiene el usuario por nombre de usuario
                return _usuarioRecetaRepositorio.GetByUsername(username);
            }

            public void Create(Usuario nuevoUsuario)
            {
                // Crea un nuevo usuario
                _usuarioRecetaRepositorio.Create(nuevoUsuario);
            }
        }
    }

}
