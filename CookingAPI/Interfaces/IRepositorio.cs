namespace CookingAPI.Interfaces
{
    public interface IRepositorio<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
    }
}
