using CookingAPI.Models;

namespace CookingAPI.InterfacesRepo
{
    public interface IUsuarioRecetaRepositorio
    {
        void CrearReceta(Receta receta);
        IEnumerable<Receta> ObtenerRecetasDelUsuario();
        void ActualizarReceta(int id, Receta recetaActualizada);
        void EliminarReceta(int id);
        Receta? ObtenerReceta(int id); // Método para crear un nuevo usuario

    }
}
