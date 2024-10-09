using CookingAPI.Models;

namespace CookingAPI.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Usuario? GetByUsername(string username);
        bool ValidateCredentials(string username, string password);
        void Create(Usuario usuario); // Método para crear un nuevo usuario

    }
}
