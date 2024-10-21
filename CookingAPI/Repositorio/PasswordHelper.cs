using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace CookingAPI.Repositorio
{
    public static class PasswordHelper
    {
        // Método para hashear una contraseña
        public static string HashPassword(string password)
        {
            // Generar un salt seguro
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derivar una subclave usando PBKDF2
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Devolver el hash y el salt concatenados para almacenamiento
            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        // Método para verificar una contraseña
        public static bool VerifyPassword(string password, string hashedPasswordWithSalt)
        {
            try
            {
                // Separar el hash y el salt
                var parts = hashedPasswordWithSalt.Split('.');
                if (parts.Length != 2) return false;

                var salt = Convert.FromBase64String(parts[0]);
                var storedHash = parts[1];

                // Volver a derivar la clave con la contraseña ingresada
                string computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                // Comparar los hashes
                return storedHash == computedHash;
            }
            catch
            {
                return false;
            }
        }
    }
}
