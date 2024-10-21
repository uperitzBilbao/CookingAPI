namespace CookingAPI.InterfacesService
{
    public interface IUserIdService
    {
        int GetUserId();
        void SetUserId(string username, int userId);
        void RemoveUserId(string username);
    }
}
