using CookingAPI.InterfacesService;
using Microsoft.AspNetCore.Mvc;

namespace CookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecetaIngredienteController : ControllerBase
    {
        private readonly IRecetaIngredienteService _recetaIngredienteService;

        public RecetaIngredienteController(IRecetaIngredienteService recetaIngredienteService)
        {
            _recetaIngredienteService = recetaIngredienteService;
        }

        // POST: api/recetaingrediente
        [HttpPost]
        public IActionResult CreateRecetaIngrediente([FromBody] RecetaIngrediente recetaIngrediente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _recetaIngredienteService.Add(recetaIngrediente);
            return CreatedAtAction(nameof(Get), new { id = recetaIngrediente.Id }, recetaIngrediente);
        }

        // GET: api/recetaingrediente
        [HttpGet]
        public ActionResult<List<RecetaIngrediente>> GetAll()
        {
            return Ok(_recetaIngredienteService.GetAll());
        }

        // GET: api/recetaingrediente/{id}
        [HttpGet("{id}")]
        public ActionResult<RecetaIngrediente> Get(int id)
        {
            var recetaIngrediente = _recetaIngredienteService.Get(id);
            if (recetaIngrediente == null)
            {
                return NotFound();
            }
            return Ok(recetaIngrediente);
        }

        // PUT: api/recetaingrediente/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RecetaIngrediente recetaIngrediente)
        {
            if (id != recetaIngrediente.Id)
            {
                return BadRequest();
            }

            _recetaIngredienteService.Update(recetaIngrediente);
            return NoContent();
        }

        // DELETE: api/recetaingrediente/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _recetaIngredienteService.Delete(id);
            return NoContent();
        }
    }
}
