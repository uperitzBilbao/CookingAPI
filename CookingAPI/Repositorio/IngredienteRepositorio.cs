using CookingAPI.DataModel;
using CookingAPI.Models;
using CookingAPI.Respositorio;

namespace CookingAPI.Repositorio
{
    public class IngredienteRepositorio : Repositorio<Ingrediente>, IIngredienteRepositorio
    {
        public IngredienteRepositorio(CookingModel context) : base(context)
        {
        }

    }
}
