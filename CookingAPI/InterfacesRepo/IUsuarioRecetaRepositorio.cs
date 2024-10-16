using CookingAPI.Models;

namespace CookingAPI.InterfacesRepo
{
    public interface IUsuarioRecetaRepositorio
    {
        void CrearReceta(int usuarioId, Receta receta);
        IEnumerable<Receta> ObtenerRecetasDelUsuario(int usuarioId);
        bool ActualizarReceta(int usuarioId, int recetaId, Receta recetaActualizada);
        bool EliminarReceta(int usuarioId, int recetaId);
        Receta? ObtenerReceta(int usuarioId, int recetaId);
    }
}
