using CookingAPI.Models;

namespace CookingAPI.InterfacesRepo
{
    public interface IIngredienteAlergenoRepositorio
    {
        void Add(IngredienteAlergeno ingredienteAlergeno);
        IngredienteAlergeno GetById(int id);
        IEnumerable<IngredienteAlergeno> GetAll();
        void Update(IngredienteAlergeno ingredienteAlergeno);
        void Delete(int id);
        void DeleteByIngrediente(int idIngrediente);
    }
}
