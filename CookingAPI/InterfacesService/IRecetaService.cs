using CookingAPI.Models;

namespace CookingAPI.InterfacesService
{
    public interface IRecetaService
    {
        IEnumerable<Receta> GetAll(); // Obtener todas las recetas
        Receta? Get(int id); // Obtener receta por ID
        IEnumerable<Receta> GetAllCompleto(); // Obtener todas las recetas con toda la información
        Receta? GetCompleto(int id); // Obtener receta completa por ID
        void Add(Receta receta); // Agregar una nueva receta
        void Update(int id, Receta updatedReceta); // Actualizar receta existente
        void Delete(int id); // Eliminar receta por ID
        IEnumerable<Receta> SearchReceta(string? nombre, int? tipoDietaId, int? tipoAlergenoId); // Buscar recetas
    }
}
