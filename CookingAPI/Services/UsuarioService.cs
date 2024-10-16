using CookingAPI.DataModel;
using CookingAPI.InterfacesService;
using CookingAPI.Models;

namespace CookingAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly CookingModel _context;

        public UsuarioService(CookingModel context)
        {
            _context = context;
        }

        public bool ValidateCredentials(string username, string password)
        {
            return _context.Usuarios.Any(u => u.Username == username && u.Password == password);
        }

        public Usuario GetByUsername(string username)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Username == username);
        }

        public int GetUserIdByUsername(string username)
        {
            var usuario = GetByUsername(username);
            return usuario?.Id ?? 0; // Retorna 0 si no se encuentra el usuario
        }

        public void Create(Usuario nuevoUsuario)
        {
            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();
        }

        public void AsociarRecetaAUsuario(UsuarioReceta usuarioReceta)
        {
            _context.UsuarioRecetas.Add(usuarioReceta);
            _context.SaveChanges();
        }
    }
}
