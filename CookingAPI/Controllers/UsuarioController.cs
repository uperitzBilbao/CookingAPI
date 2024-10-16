using CookingAPI.InterfacesService;
using CookingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public UsuarioController(IUsuarioService usuarioService, IConfiguration configuration, IMemoryCache cache)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
            _cache = cache;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (_usuarioService.ValidateCredentials(request.Username, request.Password))
            {
                var claims = new[] { new Claim(ClaimTypes.Name, request.Username) };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenExpiryMinutes = int.Parse(_configuration["Jwt:ExpiryMinutes"]);
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(tokenExpiryMinutes),
                    signingCredentials: creds);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                // Guarda el ID de usuario en caché
                var userId = _usuarioService.GetUserIdByUsername(request.Username);
                _cache.Set(request.Username, userId, TimeSpan.FromMinutes(tokenExpiryMinutes));

                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (_usuarioService.GetByUsername(request.Username) != null)
            {
                return Conflict("El nombre de usuario ya está en uso.");
            }

            var nuevoUsuario = new Usuario { Username = request.Username, Password = request.Password };
            _usuarioService.Create(nuevoUsuario);
            return CreatedAtAction(nameof(Login), new { username = nuevoUsuario.Username }, nuevoUsuario);
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var username = User.Identity.Name;
            if (username != null)
            {
                _cache.Remove(username); // Elimina el usuario de la caché
            }
            return NoContent();
        }
    }

    public class LoginRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
