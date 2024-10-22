using CookingAPI.Constantes;
using CookingAPI.InterfacesService;
using Microsoft.Extensions.Caching.Memory;

namespace CookingAPI.Services
{
    public class UserIdService : IUserIdService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<UserIdService> _logger;

        public UserIdService(IMemoryCache cache, ILogger<UserIdService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public int GetUserId()
        {
            try
            {
                if (_cache.TryGetValue("UserId", out int userId))
                {
                    _logger.LogInformation(Mensajes.Logs.USERID_RECUPERADO, userId);
                    return userId;
                }
                _logger.LogWarning(Mensajes.Logs.USERID_NO_ENCONTRADO);
                return 0; // Si no se encuentra el userId, devolvemos 0
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_RECUPERAR_USERID);
                throw;
            }
        }

        public void SetUserId(string username, int userId)
        {
            try
            {
                _cache.Set("UserId", userId, TimeSpan.FromHours(1)); // El userId se almacena por una hora
                _logger.LogInformation(Mensajes.Logs.ESTABLECER_USERID, userId, username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_ESTABLECER_USERID, username);
                throw;
            }
        }

        public void RemoveUserId(string username)
        {
            try
            {
                _cache.Remove("UserId");
                _logger.LogInformation(Mensajes.Logs.REMOVER_USERID, username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Mensajes.Error.ERROR_REMOVER_USERID, username);
                throw;
            }
        }
    }
}
