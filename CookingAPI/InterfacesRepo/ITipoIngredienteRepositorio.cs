namespace CookingAPI.InterfacesRepo
{
    public interface ITipoIngredienteRepositorio
    {
        void Add(TipoIngrediente tipoIngrediente);
        TipoIngrediente GetById(int id);
        TipoIngrediente GetByNombre(string nombre);
        IEnumerable<TipoIngrediente> GetAll();
        void Update(TipoIngrediente tipoIngrediente);
        void Delete(int id);
    }
}
