using CookingAPI.Models;

namespace CookingAPI.Interfaces
{
    public interface IRecetaService
    {
        List<Receta> GetAll();
        Receta? Get(int id);
        List<Receta> GetAllCompleto();
        Receta? GetCompleto(int id);
        void Add(Receta receta);
        void Update(int id, Receta updatedReceta);
        void Delete(int id);
        List<Receta> Search(string? nombre, int? tipoDietaId, int? tipoAlergenoId);
    }
}
