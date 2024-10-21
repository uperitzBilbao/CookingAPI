using CookingAPI.Models;

namespace CookingAPI.InterfacesService
{
    public interface IIngredienteService
    {
        IEnumerable<Ingrediente> GetAll();
        Ingrediente? Get(int id);
        void Add(Ingrediente ingrediente);
        void Update(int id, Ingrediente updatedIngrediente);
        void Delete(int id);
    }
}
