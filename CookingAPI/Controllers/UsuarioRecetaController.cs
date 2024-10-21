using CookingAPI.Constantes;
using CookingAPI.InterfacesService;
using CookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioRecetaController : ControllerBase
    {
        private readonly IUsuarioRecetaService _usuarioRecetaService;
        private readonly IUserIdService _userIdService;
        private readonly ILogger<UsuarioRecetaController> _logger;

        public UsuarioRecetaController(IUsuarioRecetaService usuarioRecetaService, IUserIdService userIdService, ILogger<UsuarioRecetaController> logger)
        {
            _usuarioRecetaService = usuarioRecetaService;
            _userIdService = userIdService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CrearReceta([FromBody] Receta receta)
        {
            try
            {
                var userId = _userIdService.GetUserId();
                if (userId == 0)
                {
                    return Unauthorized(new ProblemDetails { Title = Mensajes.Error.ERROR_NO_AUTENTICADO });
                }

                receta.UsuarioRecetas = new List<UsuarioReceta>
                {
                    new UsuarioReceta
                    {
                        UsuarioId = userId,
                        RecetaId = receta.IdReceta
                    }
                };

                _usuarioRecetaService.CrearReceta(receta);
                _logger.LogInformation(Mensajes.Logs.CREAR_RECETA_USUARIO, userId, receta.Nombre);

                return CreatedAtAction(nameof(ObtenerReceta), new { id = receta.IdReceta }, receta);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_CREAR_RECETA_USUARIO);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Logs.ERROR_CREAR_RECETA });
            }
        }

        [HttpGet]
        public ActionResult<List<Receta>> ObtenerRecetasDelUsuario()
        {
            try
            {
                var userId = _userIdService.GetUserId();
                if (userId == 0)
                {
                    return Unauthorized(new ProblemDetails { Title = Mensajes.Error.ERROR_NO_AUTENTICADO });
                }

                var recetas = _usuarioRecetaService.ObtenerRecetasDelUsuario();
                return Ok(recetas);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_OBTENER_RECETAS_USUARIO);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Logs.ERROR_OBTENER_RECETAS_USUARIO });
            }
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarReceta(int id, [FromBody] Receta recetaActualizada)
        {
            try
            {
                var actualizado = _usuarioRecetaService.ActualizarReceta(id, recetaActualizada);
                if (!actualizado)
                {
                    return Unauthorized(new ProblemDetails { Title = Mensajes.Logs.ERROR_NO_PERMISO_ACTUALIZAR });
                }

                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_ACTUALIZAR_RECETA, id);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Logs.ERROR_ACTUALIZAR_RECETA });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarReceta(int id)
        {
            try
            {
                var eliminado = _usuarioRecetaService.EliminarReceta(id);
                if (!eliminado)
                {
                    return Unauthorized(new ProblemDetails { Title = Mensajes.Error.ERROR_NO_PERMISO_ELIMINAR });
                }

                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_ELIMINAR_RECETA, id);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Logs.ERROR_ELIMINAR_RECETA });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Receta> ObtenerReceta(int id)
        {
            try
            {
                var receta = _usuarioRecetaService.ObtenerReceta(id);
                if (receta == null)
                {
                    return NotFound(new ProblemDetails { Title = Mensajes.Logs.RECETA_NO_ENCONTRADA, Detail = $"Receta con ID {id} no encontrada" });
                }

                return Ok(receta);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_OBTENER_RECETA_ID, id);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Logs.ERROR_OBTENER_RECETA });
            }
        }
    }
}
