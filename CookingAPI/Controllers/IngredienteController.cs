using CookingAPI.Constantes;
using CookingAPI.InterfacesService;
using CookingAPI.Models;
using CookingAPI.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IngredienteController : ControllerBase
    {
        private readonly IIngredienteService _ingredienteService;
        private readonly ILogger<IngredienteController> _logger;

        public IngredienteController(IIngredienteService ingredienteService, ILogger<IngredienteController> logger)
        {
            _ingredienteService = ingredienteService;
            _logger = logger;
        }

        // GET: api/ingrediente
        [HttpGet]
        public ActionResult<List<Ingrediente>> GetAll()
        {
            try
            {
                var ingredientes = _ingredienteService.GetAll();
                return Ok(ingredientes);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_INGREDIENTES);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Error.ERROR_OBTENER_INGREDIENTES });
            }
        }

        // GET: api/ingrediente/{id}
        [HttpGet("{id}")]
        public ActionResult<Ingrediente> Get(int id)
        {
            try
            {
                var ingrediente = _ingredienteService.Get(id);
                if (ingrediente == null)
                {
                    return NotFound(new ProblemDetails { Title = Mensajes.Logs.INGREDIENTE_NO_ENCONTRADO });
                }
                return Ok(ingrediente);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_OBTENER_INGREDIENTE_ID, id);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Error.ERROR_OBTENER_INGREDIENTE });
            }
        }

        // POST: api/ingrediente
        [HttpPost]
        public ActionResult<Ingrediente> CreateIngrediente([FromBody] IngredienteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _ingredienteService.Add(request);
                _logger.LogInformation(Mensajes.Logs.CREAR_INGREDIENTE, request.Nombre);
                return CreatedAtAction(nameof(Get), request);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_CREAR_INGREDIENTE);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Error.ERROR_CREAR_INGREDIENTE });
            }
        }

        // PUT: api/ingrediente/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] IngredienteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingIngrediente = _ingredienteService.Get(id);
                if (existingIngrediente == null)
                {
                    return NotFound(new ProblemDetails { Title = Mensajes.Logs.INGREDIENTE_NO_ENCONTRADO });
                }

                _ingredienteService.Update(id, request);
                _logger.LogInformation(Mensajes.Logs.ACTUALIZAR_INGREDIENTE, request.Nombre, id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_ACTUALIZAR_INGREDIENTE, id);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Error.ERROR_ACTUALIZAR_INGREDIENTE });
            }
        }

        // DELETE: api/ingrediente/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ingrediente = _ingredienteService.Get(id);
                if (ingrediente == null)
                {
                    return NotFound(new ProblemDetails { Title = Mensajes.Logs.INGREDIENTE_NO_ENCONTRADO });
                }

                _ingredienteService.Delete(id);
                _logger.LogInformation(Mensajes.Logs.ELIMINAR_INGREDIENTE, id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_ELIMINAR_INGREDIENTE, id);
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Error.ERROR_ELIMINAR_INGREDIENTE });
            }
        }
    }
}
