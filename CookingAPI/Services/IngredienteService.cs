using CookingAPI.DataModel;
using CookingAPI.Interfaces;
using CookingAPI.Models;

namespace CookingAPI.Services
{
    public class IngredienteService : IIngredienteService
    {
        private readonly CookingModel _context;

        public IngredienteService(CookingModel context)
        {
            _context = context;
        }

        public List<Ingrediente> GetAll()
        {
            return _context.Ingredientes.ToList();
        }

        public Ingrediente? Get(int id)
        {
            return _context.Ingredientes.FirstOrDefault(i => i.IdIngrediente == id);
        }

        public void Add(Ingrediente ingrediente)
        {
            // Establecer el ID como autoincremental por la base de datos
            _context.Ingredientes.Add(ingrediente);
            _context.SaveChanges();
        }

        public void Update(int id, Ingrediente updatedIngrediente)
        {
            var ingrediente = Get(id);
            if (ingrediente != null)
            {
                ingrediente.Nombre = updatedIngrediente.Nombre;
                ingrediente.IdTipoIngrediente = updatedIngrediente.IdTipoIngrediente;

                _context.Ingredientes.Update(ingrediente);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var ingrediente = Get(id);
            if (ingrediente != null)
            {
                _context.Ingredientes.Remove(ingrediente);
                _context.SaveChanges();
            }
        }

        // Obtener los alérgenos de un ingrediente
        public List<TipoAlergeno> GetAlergenos(int id)
        {
            var ingrediente = Get(id);
            return ingrediente?.IngredienteAlergenos.Select(ia => ia.TipoAlergeno).ToList() ?? new List<TipoAlergeno>();
        }
    }
}
