using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;

namespace CookingAPI.Repositorio
{
    public class TipoIngredienteRepositorio : ITipoIngredienteRepositorio
    {
        private readonly CookingModel _context;

        public TipoIngredienteRepositorio(CookingModel context)
        {
            _context = context;
        }

        public void Add(TipoIngrediente tipoIngrediente)
        {
            _context.TiposIngrediente.Add(tipoIngrediente);
            _context.SaveChanges();
        }

        public TipoIngrediente GetById(int id)
        {
            return _context.TiposIngrediente.Find(id);
        }

        public TipoIngrediente GetByNombre(string nombre)
        {
            return _context.TiposIngrediente.FirstOrDefault(ti => ti.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<TipoIngrediente> GetAll()
        {
            return _context.TiposIngrediente.ToList();
        }

        public void Update(TipoIngrediente tipoIngrediente)
        {
            _context.TiposIngrediente.Update(tipoIngrediente);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tipoIngrediente = GetById(id);
            if (tipoIngrediente != null)
            {
                _context.TiposIngrediente.Remove(tipoIngrediente);
                _context.SaveChanges();
            }
        }
    }
}
