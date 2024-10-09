namespace CookingAPI.Repositorio
{
    using BCrypt.Net;

    public class PasswordHelper
    {
        // Hashear la contraseña
        public static string HashPassword(string password)
        {
            return BCrypt.HashPassword(password);
        }

        // Verificar la contraseña
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Verify(password, hashedPassword);
        }
    }

}
