using CookingAPI.Models;

namespace CookingAPI.InterfacesService
{
    public interface IUsuarioRecetaService
    {
        void CrearReceta(Receta receta);
        IEnumerable<Receta> ObtenerRecetasDelUsuario();
        bool ActualizarReceta(int recetaId, Receta recetaActualizada);
        bool EliminarReceta(int recetaId);
        Receta? ObtenerReceta(int recetaId);
    }
}
