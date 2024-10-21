using CookingAPI.Models;

namespace CookingAPI.InterfacesRepo
{
    public interface IUsuarioRecetaRepositorio
    {
        void CrearReceta(Receta receta);
        List<Receta> ObtenerRecetasDelUsuario();
        bool ActualizarReceta(int recetaId, Receta recetaActualizada);
        bool EliminarReceta(int recetaId);
        Receta? ObtenerReceta(int recetaId);
    }
}
