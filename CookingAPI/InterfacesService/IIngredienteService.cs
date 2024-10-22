using CookingAPI.Models;
using CookingAPI.Requests;

namespace CookingAPI.InterfacesService
{
    public interface IIngredienteService
    {
        IEnumerable<Ingrediente> GetAll();
        Ingrediente? Get(int id);
        void Add(IngredienteRequest request);
        void Update(int id, IngredienteRequest request);
        void Delete(int id);
    }
}
