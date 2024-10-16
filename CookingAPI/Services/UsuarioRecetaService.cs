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

        public void CrearReceta(int usuarioId, Receta receta)
        {
            _usuarioRecetaRepositorio.CrearReceta(usuarioId, receta);
        }

        public IEnumerable<Receta> ObtenerRecetasDelUsuario(int usuarioId)
        {
            return _usuarioRecetaRepositorio.ObtenerRecetasDelUsuario(usuarioId);
        }

        public bool ActualizarReceta(int usuarioId, int recetaId, Receta recetaActualizada)
        {
            return _usuarioRecetaRepositorio.ActualizarReceta(usuarioId, recetaId, recetaActualizada);
        }

        public bool EliminarReceta(int usuarioId, int recetaId)
        {
            return _usuarioRecetaRepositorio.EliminarReceta(usuarioId, recetaId);
        }

        public Receta? ObtenerReceta(int usuarioId, int recetaId)
        {
            return _usuarioRecetaRepositorio.ObtenerReceta(usuarioId, recetaId);
        }
    }
}
