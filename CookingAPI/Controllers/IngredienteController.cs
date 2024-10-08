using CookingAPI.Models;
using CookingAPI.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace CookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredienteController : ControllerBase
    {
        private readonly IngredienteRepositorio _ingredienteRepositorio;
        private readonly ILogger<IngredienteController> _logger;

        public IngredienteController(IngredienteRepositorio ingredienteRepositorio, ILogger<IngredienteController> logger)
        {
            _ingredienteRepositorio = ingredienteRepositorio;
            _logger = logger;
        }

        // GET: api/ingrediente
        [HttpGet]
        public ActionResult<List<Ingrediente>> GetAll()
        {
            var ingredientes = _ingredienteRepositorio.GetAll();
            return Ok(ingredientes);
        }

        // GET: api/ingrediente/{id}
        [HttpGet("{id}")]
        public ActionResult<Ingrediente> Get(int id)
        {
            var ingrediente = _ingredienteRepositorio.Get(id);
            if (ingrediente == null)
            {
                return NotFound();
            }
            return Ok(ingrediente);
        }

        // POST: api/ingrediente
        [HttpPost]
        public ActionResult<Ingrediente> CreateIngrediente([FromBody] Ingrediente ingrediente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Manejo de errores de validación
            }

            _ingredienteRepositorio.Add(ingrediente);
            _logger.LogInformation($"Ingrediente creado: {ingrediente.Nombre} con ID {ingrediente.IdIngrediente}.");
            return CreatedAtAction(nameof(Get), new { id = ingrediente.IdIngrediente }, ingrediente);
        }

        // PUT: api/ingrediente/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Ingrediente ingrediente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Manejo de errores de validación
            }

            var existingIngrediente = _ingredienteRepositorio.Get(id);
            if (existingIngrediente == null)
            {
                return NotFound();
            }

            _ingredienteRepositorio.Update(ingrediente);
            _logger.LogInformation($"Ingrediente actualizado: {ingrediente.Nombre} con ID {ingrediente.IdIngrediente}.");
            return NoContent();
        }

        // DELETE: api/ingrediente/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ingrediente = _ingredienteRepositorio.Get(id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            _ingredienteRepositorio.Delete(id);
            _logger.LogInformation($"Ingrediente eliminado con ID {id}.");
            return NoContent();
        }
    }
}
