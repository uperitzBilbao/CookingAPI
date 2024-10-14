using CookingAPI.Models;

namespace CookingAPI.InterfacesService
{
    public interface IUsuarioRecetaService
    {
        void CrearReceta(Receta receta);
        IEnumerable<Receta> ObtenerRecetasDelUsuario();
        void ActualizarReceta(int id, Receta recetaActualizada);
        void EliminarReceta(int id);
        Receta? ObtenerReceta(int id);
    }
}
