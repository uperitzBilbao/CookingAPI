namespace CookingAPI.InterfacesRepo
{
    public interface ITipoDietaRepositorio
    {
        void Add(TipoDieta tipoDieta);
        TipoDieta GetById(int id);
        TipoDieta GetByNombre(string nombre);
        IEnumerable<TipoDieta> GetAll();
        void Update(TipoDieta tipoDieta);
        void Delete(int id);
    }
}
