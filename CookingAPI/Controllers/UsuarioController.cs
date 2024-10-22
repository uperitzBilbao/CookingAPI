using CookingAPI.Constantes;
using CookingAPI.InterfacesService;
using CookingAPI.Models;
using CookingAPI.Repositorio;
using CookingAPI.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IUserIdService _userIdService;

        public UsuarioController(IUsuarioService usuarioService, IConfiguration configuration, IUserIdService userIdService)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
            _userIdService = userIdService;
        }

        // POST: api/usuario/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuario = _usuarioService.GetByUsername(request.Username);
                if (usuario == null || !PasswordHelper.VerifyPassword(request.Password, usuario.Password))
                {
                    return Unauthorized(new ProblemDetails { Title = Mensajes.Logs.USUARIO_CONTRASENA_INCORRECTA });
                }

                // Crear los claims
                var claims = new[] { new Claim(ClaimTypes.Name, request.Username) };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Crear el token
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                // Guardar el ID de usuario en caché
                var userId = _usuarioService.GetUserIdByUsername(request.Username);
                _userIdService.SetUserId(request.Username, userId);

                return Ok(new { Token = tokenString });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Error.ERROR_SERVIDOR, Detail = ex.Message });
            }
        }

        // POST: api/usuario/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (_usuarioService.GetByUsername(request.Username) != null)
                {
                    return Conflict(new ProblemDetails { Title = Mensajes.Logs.USUARIO_EN_USO });
                }

                var nuevoUsuario = new Usuario
                {
                    Username = request.Username,
                    Password = PasswordHelper.HashPassword(request.Password)
                };

                _usuarioService.Create(nuevoUsuario);
                return CreatedAtAction(nameof(Login), new { username = nuevoUsuario.Username }, nuevoUsuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Error.ERROR_CREAR_USUARIO, Detail = ex.Message });
            }
        }

        // POST: api/usuario/logout
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            try
            {
                var username = User.Identity.Name;
                if (username != null)
                {
                    _userIdService.RemoveUserId(username);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails { Title = Mensajes.Error.ERROR_CERRAR_SESION, Detail = ex.Message });
            }
        }
    }
}
