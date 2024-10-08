using CookingAPI.DataModel;
using CookingAPI.Models;
using CookingAPI.Respositorio;

namespace CookingAPI.Repositorio
{
    public class IngredienteRepositorio : Repositorio<Ingrediente>, IIngredienteRepositorio
    {
        private readonly ILogger<IngredienteRepositorio> _logger;

        public IngredienteRepositorio(CookingModel context, ILogger<IngredienteRepositorio> logger) : base(context, logger)
        {
            _logger = logger;
        }


    }
}
