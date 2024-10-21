using CookingAPI.Constantes;
using CookingAPI.InterfacesService;
using CookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecetaController : ControllerBase
    {
        private readonly IRecetaService _recetaService;
        private readonly ILogger<RecetaController> _logger;

        public RecetaController(IRecetaService recetaService, ILogger<RecetaController> logger)
        {
            _recetaService = recetaService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Receta>> GetAll()
        {
            try
            {
                var recetas = _recetaService.GetAll();
                if (recetas == null || !recetas.Any())
                {
                    return NotFound(new ProblemDetails { Title = Mensajes.Logs.NO_HAY_RECETAS });
                }
                return Ok(recetas);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_OBTENER_RECETAS);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Logs.ERROR_OBTENER_RECETAS });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Receta> Get(int id)
        {
            try
            {
                var receta = _recetaService.Get(id);
                if (receta == null)
                {
                    return NotFound(new ProblemDetails { Title = Mensajes.Logs.RECETA_NO_ENCONTRADA, Detail = $"Receta con ID {id} no encontrada." });
                }
                return Ok(receta);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_OBTENER_RECETA_ID, id);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Logs.ERROR_OBTENER_RECETA });
            }
        }

        [HttpPost]
        public IActionResult CreateReceta([FromBody] Receta receta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _recetaService.Add(receta);
                _logger.LogInformation(Mensajes.Logs.CREAR_RECETA, receta.Nombre, receta.IdReceta);
                return CreatedAtAction(nameof(Get), new { id = receta.IdReceta }, receta);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_CREAR_RECETA);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Logs.ERROR_CREAR_RECETA });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Receta receta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingReceta = _recetaService.Get(id);
                if (existingReceta == null)
                {
                    return NotFound(new ProblemDetails { Title = Mensajes.Logs.RECETA_NO_ENCONTRADA, Detail = $"Receta con ID {id} no encontrada." });
                }

                _recetaService.Update(id, receta);
                _logger.LogInformation(Mensajes.Logs.ACTUALIZAR_RECETA, receta.Nombre, id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_ACTUALIZAR_RECETA, id);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Logs.ERROR_ACTUALIZAR_RECETA });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingReceta = _recetaService.Get(id);
                if (existingReceta == null)
                {
                    return NotFound(new ProblemDetails { Title = Mensajes.Logs.RECETA_NO_ENCONTRADA, Detail = $"Receta con ID {id} no encontrada." });
                }

                _recetaService.Delete(id);
                _logger.LogInformation(Mensajes.Logs.ELIMINAR_RECETA, id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_ELIMINAR_RECETA, id);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Logs.ERROR_ELIMINAR_RECETA });
            }
        }

        [HttpGet("search")]
        public ActionResult<List<Receta>> Search(string? nombre = null, int? tipoDietaId = null, int? tipoAlergenoId = null)
        {
            try
            {
                var recetas = _recetaService.SearchReceta(nombre, tipoDietaId, tipoAlergenoId);
                if (recetas == null || !recetas.Any())
                {
                    return NotFound(new ProblemDetails { Title = Mensajes.Logs.NO_RECETAS_CRITERIOS });
                }
                return Ok(recetas);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Logs.ERROR_BUSQUEDA_RECETAS);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Logs.ERROR_BUSQUEDA_RECETAS });
            }
        }
    }
}
