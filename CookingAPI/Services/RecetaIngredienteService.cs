using CookingAPI.InterfacesRepo;
using CookingAPI.InterfacesService;

namespace CookingAPI.Services
{
    public class RecetaIngredienteService : IRecetaIngredienteService
    {
        private readonly IRecetaIngredienteRepositorio _recetaIngredienteRepositorio;
        private readonly ILogger<RecetaIngredienteService> _logger;

        public RecetaIngredienteService(IRecetaIngredienteRepositorio recetaIngredienteRepositorio, ILogger<RecetaIngredienteService> logger)
        {
            _recetaIngredienteRepositorio = recetaIngredienteRepositorio;
            _logger = logger;
        }

        public void Add(RecetaIngrediente recetaIngrediente)
        {
            _logger.LogInformation($"Adding ingredient to recipe: {recetaIngrediente.IdReceta}, Ingredient ID: {recetaIngrediente.IdIngrediente}, Quantity: {recetaIngrediente.Cantidad}");
            _recetaIngredienteRepositorio.Add(recetaIngrediente);
        }

        public IEnumerable<RecetaIngrediente> GetAll()
        {
            return _recetaIngredienteRepositorio.GetAll();
        }

        public RecetaIngrediente? Get(int id)
        {
            return _recetaIngredienteRepositorio.Get(id);
        }

        public void Update(RecetaIngrediente recetaIngrediente)
        {
            _recetaIngredienteRepositorio.Update(recetaIngrediente.Id, recetaIngrediente);
        }

        public void Delete(int id)
        {
            _recetaIngredienteRepositorio.Delete(id);
        }
    }
}
