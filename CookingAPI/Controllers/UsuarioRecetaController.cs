using CookingAPI.InterfacesService;
using CookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookingAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "CustomPolicy")]
    public class UsuarioRecetaController : ControllerBase
    {
        private readonly IUsuarioRecetaService _usuarioRecetaService;
        private readonly ILogger<UsuarioRecetaController> _logger;

        public UsuarioRecetaController(IUsuarioRecetaService usuarioRecetaService, ILogger<UsuarioRecetaController> logger)
        {
            _usuarioRecetaService = usuarioRecetaService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CrearReceta([FromBody] Receta receta)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (usuarioId == null)
            {
                return Unauthorized();
            }

            receta.IdReceta = 0; // Reinicia el ID de la receta antes de agregarla
            _usuarioRecetaService.CrearReceta(int.Parse(usuarioId), receta);

            return CreatedAtAction(nameof(ObtenerReceta), new { id = receta.IdReceta }, receta);
        }

        [HttpGet]
        public ActionResult<List<Receta>> ObtenerRecetasDelUsuario()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (usuarioId == null)
            {
                return Unauthorized();
            }

            var recetas = _usuarioRecetaService.ObtenerRecetasDelUsuario(int.Parse(usuarioId));
            return Ok(recetas);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarReceta(int id, [FromBody] Receta recetaActualizada)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (usuarioId == null)
            {
                return Unauthorized();
            }

            var actualizado = _usuarioRecetaService.ActualizarReceta(int.Parse(usuarioId), id, recetaActualizada);
            if (!actualizado)
            {
                return Forbid(); // El usuario no tiene permiso para actualizar la receta
            }

            return NoContent(); // Actualización exitosa
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarReceta(int id)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (usuarioId == null)
            {
                return Unauthorized();
            }

            var eliminado = _usuarioRecetaService.EliminarReceta(int.Parse(usuarioId), id);
            if (!eliminado)
            {
                return Forbid(); // El usuario no tiene permiso para eliminar la receta
            }

            return NoContent(); // Eliminación exitosa
        }

        [HttpGet("{id}")]
        public ActionResult<Receta> ObtenerReceta(int id)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (usuarioId == null)
            {
                return Unauthorized();
            }

            var receta = _usuarioRecetaService.ObtenerReceta(int.Parse(usuarioId), id);
            if (receta == null)
            {
                return NotFound();
            }

            return Ok(receta);
        }
    }
}
