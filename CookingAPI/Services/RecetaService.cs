using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;
using CookingAPI.InterfacesService;
using CookingAPI.Models;
using Microsoft.Extensions.Caching.Memory; // Importar el espacio de nombres para caché
using System.Security.Claims;

namespace CookingAPI.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly CookingModel _context;
        private readonly ILogger<RecetaService> _logger;
        private readonly IRecetaRepositorio _recetaRepositorio;
        private readonly IMemoryCache _cache; // Agregar la memoria caché

        // Constructor para inyectar el contexto de la base de datos, el logger y la caché
        public RecetaService(IRecetaRepositorio recetaRepositorio, CookingModel context, ILogger<RecetaService> logger, IMemoryCache cache)
        {
            _context = context;
            _logger = logger;
            _recetaRepositorio = recetaRepositorio;
            _cache = cache; // Inicializar la caché
        }

        // Obtener todas las recetas, incluyendo los ingredientes
        public IEnumerable<Receta> GetAll()
        {
            // Intentar obtener recetas de la caché
            if (!_cache.TryGetValue("recetas", out List<Receta> recetas))
            {
                recetas = _recetaRepositorio.GetAll().ToList();
                // Almacenar las recetas en la caché
                _cache.Set("recetas", recetas);
            }
            return recetas;
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

        // Obtener una receta por su ID con toda la información relacionada
        public Receta? GetCompleto(int id)
        {
            return _recetaRepositorio.GetRecetaCompleta(id);
        }

        // Agregar una nueva receta (incluyendo RecetaIngredientes)
        public void Add(Receta receta)
        {
            var userId = GetCurrentUserId(); // Obtener ID del usuario logueado
            if (userId != null)
            {
                // Asociar receta al usuario
                receta.UsuarioRecetas.Add(new UsuarioReceta { UsuarioId = userId.Value, Receta = receta });
            }

            _recetaRepositorio.Add(receta);
            _cache.Remove("recetas"); // Eliminar la caché para actualizar
        }

        // Actualizar una receta existente (incluyendo RecetaIngredientes)
        public void Update(int id, Receta updatedReceta)
        {
            _recetaRepositorio.Update(id, updatedReceta);
            _cache.Remove("recetas"); // Eliminar la caché para actualizar
        }

        // Eliminar una receta por su ID
        public void Delete(int id)
        {
            _recetaRepositorio.Delete(id);
            _cache.Remove("recetas"); // Eliminar la caché para actualizar
        }

        // Buscar recetas por nombre, tipo de dieta o alérgenos
        public IEnumerable<Receta> SearchReceta(string? nombre, int? tipoDietaId, int? tipoAlergenoId)
        {
            return _recetaRepositorio.SearchRecetas(nombre, tipoDietaId, tipoAlergenoId); // Devolver la lista final de recetas
        }

        // Método para obtener el ID del usuario logueado
        private int? GetCurrentUserId()
        {
            var claimsIdentity = (ClaimsIdentity)ClaimsPrincipal.Current.Identity;
            var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? (int?)Convert.ToInt32(userIdClaim.Value) : null;
        }
    }
}
