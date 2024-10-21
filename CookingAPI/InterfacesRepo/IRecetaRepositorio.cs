using CookingAPI.Models;

namespace CookingAPI.InterfacesRepo
{
    public interface IRecetaRepositorio : IRepositorio<Receta>
    {
        Receta GetRecetaCompleta(int id);
        List<Receta> GetAllRecetasCompletas();
        List<Receta> SearchRecetas(string? nombre, int? tipoDietaId, int? tipoAlergenoId);
    }
}
