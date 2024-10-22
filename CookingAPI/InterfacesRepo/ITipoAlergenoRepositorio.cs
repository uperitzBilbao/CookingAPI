namespace CookingAPI.InterfacesRepo
{
    public interface ITipoAlergenoRepositorio
    {
        //void Add(TipoAlergeno tipoAlergeno);
        TipoAlergeno GetById(int id);
        TipoAlergeno GetByNombre(string nombre);
        IEnumerable<TipoAlergeno> GetAll();
        //void Update(TipoAlergeno tipoAlergeno);
        //void Delete(int id);
    }
}

