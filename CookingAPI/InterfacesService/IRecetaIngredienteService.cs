namespace CookingAPI.InterfacesService
{
    public interface IRecetaIngredienteService
    {
        void Add(RecetaIngrediente recetaIngrediente);
        IEnumerable<RecetaIngrediente> GetAll();
        RecetaIngrediente? Get(int id);
        void Update(RecetaIngrediente recetaIngrediente);
        void Delete(int id);
    }
}
