using CookingAPI.InterfacesService;
using CookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "CustomPolicy")]
    public class RecetaController : ControllerBase
    {
        private readonly IRecetaService _recetaService;
        private readonly ILogger<RecetaController> _logger;

        public RecetaController(IRecetaService recetaService, ILogger<RecetaController> logger)
        {
            _recetaService = recetaService;
            _logger = logger;
        }

        // Obtener todas las recetas
        [HttpGet]
        public ActionResult<List<Receta>> GetAll()
        {
            var recetas = _recetaService.GetAll();
            if (recetas == null || !recetas.Any())
            {
                return NotFound(new ProblemDetails { Title = "No hay recetas disponibles." });
            }
            return Ok(recetas);
        }

        // Obtener una receta por su ID
        [HttpGet("{id}")]
        public ActionResult<Receta> Get(int id)
        {
            var receta = _recetaService.Get(id);
            if (receta == null)
            {
                return NotFound(new ProblemDetails { Title = $"Receta con ID {id} no encontrada." });
            }
            return Ok(receta);
        }

        // Obtener todas las recetas con toda la información relacionada
        [HttpGet("todo")]
        public ActionResult<List<Receta>> GetCompleto()
        {
            var recetas = _recetaService.GetAllCompleto();
            if (recetas == null || !recetas.Any())
            {
                return NotFound(new ProblemDetails { Title = "No hay recetas disponibles." });
            }
            return Ok(recetas);
        }

        // Obtener una receta por su ID con toda la información relacionada
        [HttpGet("todo/{id}")]
        public ActionResult<Receta> GetCompleto(int id)
        {
            var receta = _recetaService.GetCompleto(id);
            if (receta == null)
            {
                return NotFound(new ProblemDetails { Title = $"Receta completa con ID {id} no encontrada." });
            }
            return Ok(receta);
        }

        // Crear una nueva receta (incluye ingredientes)
        [HttpPost]
        public IActionResult CreateReceta([FromBody] Receta receta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _recetaService.Add(receta);
            _logger.LogInformation($"Receta creada: {receta.Nombre} con ID {receta.IdReceta}.");
            return CreatedAtAction(nameof(Get), new { id = receta.IdReceta }, receta);
        }

        // Actualizar una receta existente (incluye ingredientes)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Receta receta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingReceta = _recetaService.Get(id);
            if (existingReceta == null)
            {
                return NotFound(new ProblemDetails { Title = $"Receta con ID {id} no encontrada." });
            }

            _recetaService.Update(id, receta);
            _logger.LogInformation($"Receta actualizada: {receta.Nombre} con ID {receta.IdReceta}.");
            return NoContent();
        }

        // Eliminar una receta por su ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingReceta = _recetaService.Get(id);
            if (existingReceta == null)
            {
                return NotFound(new ProblemDetails { Title = $"Receta con ID {id} no encontrada." });
            }

            _recetaService.Delete(id);
            _logger.LogInformation($"Receta eliminada con ID {id}.");
            return NoContent();
        }

        // Buscar recetas por nombre, tipo de dieta o alérgenos
        [HttpGet("search")]
        public ActionResult<List<Receta>> Search(string? nombre = null, int? tipoDietaId = null, int? tipoAlergenoId = null)
        {
            var recetas = _recetaService.SearchReceta(nombre, tipoDietaId, tipoAlergenoId);
            if (recetas == null || !recetas.Any())
            {
                return NotFound(new ProblemDetails { Title = "No se encontraron recetas con los criterios especificados." });
            }
            return Ok(recetas);
        }
    }
}
