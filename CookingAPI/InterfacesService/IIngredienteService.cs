using CookingAPI.Requests;
using CookingAPI.Responses;

namespace CookingAPI.InterfacesService
{
    public interface IIngredienteService
    {
        IEnumerable<IngredienteResponse> GetAll();
        IngredienteResponse? Get(int id);
        void Add(IngredienteRequest request);
        void Update(int id, IngredienteRequest request);
        void Delete(int id);
    }
}
