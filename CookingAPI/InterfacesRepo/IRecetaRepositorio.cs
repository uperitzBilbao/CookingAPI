using CookingAPI.Models;

namespace CookingAPI.InterfacesRepo
{
    public interface IRecetaRepositorio : IRepositorio<Receta>
    {
        Receta GetRecetaCompleta(int id); // Obtener receta completa por ID
        List<Receta> GetAllRecetasCompletas(); // Obtener todas las recetas con detalles completos
        List<Receta> SearchRecetas(string? nombre, int? tipoDietaId, int? tipoAlergenoId); // Buscar recetas
        List<Receta> GetRecetasPorUsuario(int usuarioId); // Obtener recetas asociadas a un usuario
    }
}
