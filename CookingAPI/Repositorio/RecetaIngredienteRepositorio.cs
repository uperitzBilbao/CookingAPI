using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;

namespace CookingAPI.Repositorio
{
    public class RecetaIngredienteRepositorio : Repositorio<RecetaIngrediente>, IRecetaIngredienteRepositorio
    {
        private readonly CookingModel _context;
        private readonly ILogger<RecetaIngredienteRepositorio> _logger;

        public RecetaIngredienteRepositorio(CookingModel context, ILogger<RecetaIngredienteRepositorio> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }


    }
}
