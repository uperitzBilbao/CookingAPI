using CookingAPI.Models;

namespace CookingAPI.InterfacesService
{
    public interface IRecetaService
    {
        IEnumerable<Receta> GetAll();
        Receta? Get(int id);
        IEnumerable<Receta> GetAllCompleto();
        Receta? GetCompleto(int id);
        void Add(Receta receta);
        void Update(int id, Receta updatedReceta);
        void Delete(int id);
        IEnumerable<Receta> SearchReceta(string? nombre, int? tipoDietaId, int? tipoAlergenoId);
    }
}
