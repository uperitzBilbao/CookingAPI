using CookingAPI.DataModel;
using CookingAPI.Interfaces;
using CookingAPI.Models;

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
