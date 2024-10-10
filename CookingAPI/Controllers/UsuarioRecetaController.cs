using CookingAPI.DataModel;
using CookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CookingAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioRecetaController : ControllerBase
    {
        private readonly CookingModel _context;

        public UsuarioRecetaController(CookingModel context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CrearReceta(Receta receta)
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

            return CreatedAtAction(nameof(ObtenerReceta), new { id = receta.IdReceta }, receta);
        }

        // Método para obtener recetas del usuario
        [HttpGet]
        public ActionResult<List<Receta>> ObtenerRecetasDelUsuario()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var recetas = _context.UsuarioRecetas
                .Where(ur => ur.UsuarioId == int.Parse(usuarioId))
                .Include(ur => ur.Receta)
                .Select(ur => ur.Receta)
                .ToList();

            return Ok(recetas);
        }

        // Método para actualizar una receta del usuario
        [HttpPut("{id}")]
        public IActionResult ActualizarReceta(int id, Receta recetaActualizada)
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
                return NotFound(); // La receta no existe
            }

            // Actualiza los campos necesarios
            receta.Nombre = recetaActualizada.Nombre;
            receta.Elaboracion = recetaActualizada.Elaboracion;
            // ... otros campos que quieras actualizar

            _context.SaveChanges();
            return NoContent(); // Respuesta sin contenido al actualizar correctamente
        }

        // Método para eliminar una receta del usuario
        [HttpDelete("{id}")]
        public IActionResult EliminarReceta(int id)
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
                return NotFound(); // La receta no existe
            }

            _context.Recetas.Remove(receta);
            _context.UsuarioRecetas.Remove(usuarioReceta); // También eliminar la relación
            _context.SaveChanges();

            return NoContent(); // Respuesta sin contenido al eliminar correctamente
        }

        // Método para obtener una receta específica del usuario
        [HttpGet("{id}")]
        public ActionResult<Receta> ObtenerReceta(int id)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var receta = _context.UsuarioRecetas
                .Where(ur => ur.UsuarioId == int.Parse(usuarioId) && ur.RecetaId == id)
                .Include(ur => ur.Receta)
                .Select(ur => ur.Receta)
                .FirstOrDefault();

            if (receta == null)
            {
                return NotFound(); // La receta no existe o no pertenece al usuario
            }

            return Ok(receta);
        }
    }
}
