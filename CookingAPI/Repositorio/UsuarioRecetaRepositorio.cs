using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;
using CookingAPI.Models;
using System.Security.Claims;

namespace CookingAPI.Repositorio
{
    public class UsuarioRecetaRepositorio : Repositorio<UsuarioReceta>, IUsuarioRecetaRepositorio
    {

        private readonly ILogger<UsuarioRecetaRepositorio> _logger;

        public UsuarioRecetaRepositorio(CookingModel context, ILogger<UsuarioRecetaRepositorio> logger) : base(context, logger)
        {
            _logger = logger;
        }


        public void CrearReceta(Receta receta)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            receta.IdReceta = 0; // Asegúrate de que no tenga un ID antes de agregarla

            _context.Recetas.Add(receta);
            _context.SaveChanges();

            // Agrega la relación en UsuarioReceta
            var usuarioReceta = new UsuarioReceta
            {
                UsuarioId = int.Parse(usuarioId),
                RecetaId = receta.IdReceta
            };

            _context.UsuarioRecetas.Add(usuarioReceta);
            _context.SaveChanges();

        }

        // Método para obtener recetas del usuario

        public List<Receta> ObtenerRecetasDelUsuario()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var recetas = _context.UsuarioRecetas
                .Where(ur => ur.UsuarioId == int.Parse(usuarioId))
                .Include(ur => ur.Receta)
                .Select(ur => ur.Receta)
                .ToList();

            return recetas;
        }

        // Método para actualizar una receta del usuario

        public void ActualizarReceta(int id, Receta recetaActualizada)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Verificar si la receta pertenece al usuario
            var usuarioReceta = _context.UsuarioRecetas
                .FirstOrDefault(ur => ur.RecetaId == id && ur.UsuarioId == int.Parse(usuarioId));

            if (usuarioReceta == null)
            {
                return Forbid(); // El usuario no tiene permiso para actualizar esta receta
            }

            var receta = _context.Recetas.Find(id);
            if (receta == null)
            {
                // La receta no existe
            }

            // Actualiza los campos necesarios
            receta.Nombre = recetaActualizada.Nombre;
            receta.Elaboracion = recetaActualizada.Elaboracion;
            // ... otros campos que quieras actualizar

            _context.SaveChanges();

        }

        // Método para eliminar una receta del usuario

        public void EliminarReceta(int id)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Verificar si la receta pertenece al usuario
            var usuarioReceta = _context.UsuarioRecetas
                .FirstOrDefault(ur => ur.RecetaId == id && ur.UsuarioId == int.Parse(usuarioId));

            if (usuarioReceta == null)
            {
                return Forbid(); // El usuario no tiene permiso para eliminar esta receta
            }

            // Eliminar la receta
            var receta = _context.Recetas.Find(id);
            if (receta == null)
            {
                // La receta no existe
            }

            _context.Recetas.Remove(receta);
            _context.UsuarioRecetas.Remove(usuarioReceta); // También eliminar la relación
            _context.SaveChanges();
        }

        // Método para obtener una receta específica del usuario

        public Receta ObtenerReceta(int id)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var receta = _context.UsuarioRecetas
                .Where(ur => ur.UsuarioId == int.Parse(usuarioId) && ur.RecetaId == id)
                .Include(ur => ur.Receta)
                .Select(ur => ur.Receta)
                .FirstOrDefault();


            return receta;
        }

    }
}
