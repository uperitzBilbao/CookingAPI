using CookingAPI.Interfaces;
using CookingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public AuthController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (_usuarioRepositorio.ValidateCredentials(request.Username, request.Password))
            {
                // Generar y devolver el token JWT aquí
                return Ok("Token generado"); // Cambia esto por el token real
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Usuario nuevoUsuario)
        {
            if (_usuarioRepositorio.GetByUsername(nuevoUsuario.Username) != null)
            {
                return Conflict("El nombre de usuario ya está en uso.");
            }

            _usuarioRepositorio.Create(nuevoUsuario);

            return CreatedAtAction(nameof(Login), new { username = nuevoUsuario.Username }, nuevoUsuario);
        }


    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
