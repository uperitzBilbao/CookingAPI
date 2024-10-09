using CookingAPI.Interfaces;
using CookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IConfiguration _configuration;

        public AuthController(IUsuarioRepositorio usuarioRepositorio, IConfiguration configuration)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (_usuarioRepositorio.ValidateCredentials(request.Username, request.Password))
            {
                // Generar el token JWT
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Username)
                };

                // Obtén la clave y otros valores de la configuración
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(90),
                    signingCredentials: creds);

                // Devolver el token en la respuesta
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    Token = tokenString
                });
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
