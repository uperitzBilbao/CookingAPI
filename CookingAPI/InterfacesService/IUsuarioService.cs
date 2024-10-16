using CookingAPI.Models;

namespace CookingAPI.InterfacesService
{
    public interface IUsuarioService
    {
        bool ValidateCredentials(string username, string password);
        Usuario GetByUsername(string username);
        int GetUserIdByUsername(string username);
        void Create(Usuario nuevoUsuario);
        void AsociarRecetaAUsuario(UsuarioReceta usuarioReceta); // Método para asociar recetas
    }
}
