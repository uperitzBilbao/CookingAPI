using CookingAPI.Models;
using CookingAPI.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController : ControllerBase
    {
        private readonly RecetaRepositorio _recetaRepositorio;
        private readonly ILogger<RecetaController> _logger;

        public RecetaController(RecetaRepositorio recetaRepositorio, ILogger<RecetaController> logger)
        {
            _recetaRepositorio = recetaRepositorio;
            _logger = logger;
        }

        // Obtener todas las recetas
        [HttpGet]
        public ActionResult<List<Receta>> GetAll()
        {
            var recetas = _recetaRepositorio.GetAll();
            if (recetas == null || !recetas.Any())
            {
                return NotFound("No hay recetas disponibles.");
            }
            return Ok(recetas);
        }

        // Obtener una receta por su ID
        [HttpGet("{id}")]
        public ActionResult<Receta> Get(int id)
        {
            var receta = _recetaRepositorio.Get(id);
            if (receta == null)
            {
                return NotFound();
            }
            return Ok(receta); // Cambiado para seguir la convención de ActionResult
        }

        // Obtener todas las recetas con toda la información relacionada
        [HttpGet("todo")]
        public ActionResult<List<Receta>> GetCompleto()
        {
            var recetas = _recetaRepositorio.GetAllRecetasCompletas();
            if (recetas == null || !recetas.Any())
            {
                return NotFound("No hay recetas disponibles.");
            }
            return Ok(recetas);
        }

        // Obtener una receta por su ID con toda la información relacionada
        [HttpGet("todo/{id}")]
        public ActionResult<Receta> GetCompleto(int id)
        {
            var receta = _recetaRepositorio.GetRecetaCompleta(id);
            if (receta == null)
            {
                return NotFound();
            }
            return Ok(receta); // Cambiado para seguir la convención de ActionResult
        }

        // Crear una nueva receta (incluye ingredientes)
        [HttpPost]
        public IActionResult CreateReceta([FromBody] Receta receta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Manejo de errores de validación
            }

            _recetaRepositorio.Add(receta);
            _logger.LogInformation($"Receta creada: {receta.Nombre} con ID {receta.IdReceta}.");
            return CreatedAtAction(nameof(Get), new { id = receta.IdReceta }, receta);
        }

        // Actualizar una receta existente (incluye ingredientes)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Receta receta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Manejo de errores de validación
            }

            var existingReceta = _recetaRepositorio.Get(id);
            if (existingReceta == null)
            {
                return NotFound();
            }

            _recetaRepositorio.Update(receta);
            _logger.LogInformation($"Receta actualizada: {receta.Nombre} con ID {receta.IdReceta}.");
            return NoContent();
        }

        // Eliminar una receta por su ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingReceta = _recetaRepositorio.Get(id);
            if (existingReceta == null)
            {
                return NotFound();
            }

            _recetaRepositorio.Delete(id);
            _logger.LogInformation($"Receta eliminada con ID {id}.");
            return NoContent();
        }

        // Buscar recetas por nombre, tipo de dieta o alérgenos
        [HttpGet("search")]
        public ActionResult<List<Receta>> Search(string? nombre = null, int? tipoDietaId = null, int? tipoAlergenoId = null)
        {
            var recetas = _recetaRepositorio.SearchRecetas(nombre, tipoDietaId, tipoAlergenoId);
            if (recetas == null || !recetas.Any())
            {
                return NotFound("No se encontraron recetas con los criterios especificados.");
            }
            return Ok(recetas);
        }
    }
}
