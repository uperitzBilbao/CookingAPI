using CookingAPI.Constantes;
using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;
using CookingAPI.InterfacesService;
using CookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Repositorio
{
    public class UsuarioRecetaRepositorio : Repositorio<UsuarioReceta>, IUsuarioRecetaRepositorio
    {
        private readonly ILogger<UsuarioRecetaRepositorio> _logger;
        private readonly IUserIdService _userIdService;

        public UsuarioRecetaRepositorio(CookingModel context, ILogger<UsuarioRecetaRepositorio> logger, IUserIdService userIdService)
            : base(context, logger)
        {
            _logger = logger;
            _userIdService = userIdService;
        }

        public void CrearReceta(Receta receta)
        {
            var userId = _userIdService.GetUserId();
            if (userId == 0)
            {
                _logger.LogError(Mensajes.Error.ERROR_USERID_NO_ENCONTRADO);
                throw new Exception(Mensajes.Error.ERROR_NO_AUTENTICADO);
            }

            receta.IdReceta = 0; // Asegurarse de que es una receta nueva

            _context.Recetas.Add(receta);
            _context.SaveChanges();

            var usuarioReceta = new UsuarioReceta
            {
                UsuarioId = userId,
                RecetaId = receta.IdReceta
            };

            _context.UsuarioRecetas.Add(usuarioReceta);
            _context.SaveChanges();
        }

        public List<Receta> ObtenerRecetasDelUsuario()
        {
            var userId = _userIdService.GetUserId();
            if (userId == 0)
            {
                _logger.LogError(Mensajes.Error.ERROR_USERID_NO_ENCONTRADO);
                throw new Exception(Mensajes.Error.ERROR_NO_AUTENTICADO);
            }

            return _context.UsuarioRecetas
                .Where(ur => ur.UsuarioId == userId)
                .Include(ur => ur.Receta)
                .Select(ur => ur.Receta)
                .ToList();
        }

        public bool ActualizarReceta(int recetaId, Receta recetaActualizada)
        {
            var userId = _userIdService.GetUserId();
            if (userId == 0)
            {
                _logger.LogError(Mensajes.Error.ERROR_USERID_NO_ENCONTRADO);
                return false;
            }

            var usuarioReceta = _context.UsuarioRecetas
                .FirstOrDefault(ur => ur.RecetaId == recetaId && ur.UsuarioId == userId);

            if (usuarioReceta == null)
            {
                _logger.LogError(Mensajes.Error.ERROR_NO_PERMISO_ACTUALIZAR);
                return false;
            }

            var receta = _context.Recetas.Find(recetaId);
            if (receta == null)
            {
                _logger.LogError(Mensajes.Error.ERROR_NO_EXISTE_RECETA);
                return false;
            }

            receta.Nombre = recetaActualizada.Nombre;
            receta.Elaboracion = recetaActualizada.Elaboracion;
            receta.Raciones = recetaActualizada.Raciones;
            receta.TiempoMinutos = recetaActualizada.TiempoMinutos;
            receta.Presentacion = recetaActualizada.Presentacion;

            _context.SaveChanges();
            return true;
        }

        public bool EliminarReceta(int recetaId)
        {
            var userId = _userIdService.GetUserId();
            if (userId == 0)
            {
                _logger.LogError(Mensajes.Error.ERROR_USERID_NO_ENCONTRADO);
                return false;
            }

            var usuarioReceta = _context.UsuarioRecetas
                .FirstOrDefault(ur => ur.RecetaId == recetaId && ur.UsuarioId == userId);

            if (usuarioReceta == null)
            {
                _logger.LogError(Mensajes.Error.ERROR_NO_PERMISO_ELIMINAR);
                return false;
            }

            var receta = _context.Recetas.Find(recetaId);
            if (receta == null)
            {
                _logger.LogError(Mensajes.Error.ERROR_NO_EXISTE_RECETA);
                return false;
            }

            _context.Recetas.Remove(receta);
            _context.UsuarioRecetas.Remove(usuarioReceta);
            _context.SaveChanges();
            return true;
        }

        public Receta? ObtenerReceta(int recetaId)
        {
            var userId = _userIdService.GetUserId();
            if (userId == 0)
            {
                _logger.LogError(Mensajes.Error.ERROR_USERID_NO_ENCONTRADO);
                return null;
            }

            return _context.UsuarioRecetas
                .Where(ur => ur.UsuarioId == userId && ur.RecetaId == recetaId)
                .Include(ur => ur.Receta)
                .Select(ur => ur.Receta)
                .FirstOrDefault();
        }
    }
}
