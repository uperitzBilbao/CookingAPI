namespace CookingAPI.InterfacesRepo
{
    public interface ITipoElaboracionRepositorio
    {
        void Add(TipoElaboracion tipoElaboracion);
        TipoElaboracion GetById(int id);
        TipoElaboracion GetByNombre(string nombre);
        IEnumerable<TipoElaboracion> GetAll();
        void Update(TipoElaboracion tipoElaboracion);
        void Delete(int id);
    }
}
