using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;
using CookingAPI.InterfacesService;
using CookingAPI.Models;

namespace CookingAPI.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly CookingModel _context;
        private readonly ILogger<RecetaService> _logger;
        private readonly IRecetaRepositorio _recetaRepositorio;

        // Constructor para inyectar el contexto de la base de datos y el logger
        public RecetaService(IRecetaRepositorio recetaRepositorio, CookingModel context, ILogger<RecetaService> logger)
        {
            _context = context;
            _logger = logger;
            _recetaRepositorio = recetaRepositorio;
        }

        // Obtener todas las recetas, incluyendo los ingredientes
        public IEnumerable<Receta> GetAll()
        {
            return _recetaRepositorio.GetAll();
        }

        // Obtener una receta por su ID
        public Receta? Get(int id)
        {

            return _recetaRepositorio.Get(id);

        }

        // Obtener todas las recetas, incluyendo los ingredientes
        public IEnumerable<Receta> GetAllCompleto()
        {

            return _recetaRepositorio.GetAllRecetasCompletas();
        }

        public Receta? GetCompleto(int id)
        {
            return _recetaRepositorio.GetRecetaCompleta(id);
        }

        // Agregar una nueva receta (incluyendo RecetaIngredientes)
        public void Add(Receta receta)
        {
            _recetaRepositorio.Add(receta);
        }

        // Actualizar una receta existente (incluyendo RecetaIngredientes)
        public void Update(int id, Receta updatedReceta)
        {

            _recetaRepositorio.Update(id, updatedReceta);
        }

        // Eliminar una receta por su ID
        public void Delete(int id)
        {

            _recetaRepositorio.Delete(id);

        }

        // Buscar recetas por nombre, tipo de dieta o alérgenos
        public IEnumerable<Receta> SearchReceta(string? nombre, int? tipoDietaId, int? tipoAlergenoId)
        {

            return _recetaRepositorio.SearchRecetas(nombre, tipoDietaId, tipoAlergenoId); // Devolver la lista final de recetas
        }
    }
}