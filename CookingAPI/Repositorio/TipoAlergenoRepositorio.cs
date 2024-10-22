using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;

namespace CookingAPI.Repositorio
{
    public class TipoAlergenoRepositorio : ITipoAlergenoRepositorio
    {
        private readonly CookingModel _context;

        public TipoAlergenoRepositorio(CookingModel context)
        {
            _context = context;
        }

        public void Add(TipoAlergeno tipoAlergeno)
        {
            _context.TiposAlergeno.Add(tipoAlergeno);
            _context.SaveChanges();
        }

        public TipoAlergeno GetById(int id)
        {
            return _context.TiposAlergeno.Find(id);
        }

        public TipoAlergeno GetByNombre(string nombre)
        {
            return _context.TiposAlergeno.FirstOrDefault(ta => ta.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<TipoAlergeno> GetAll()
        {
            return _context.TiposAlergeno.ToList();
        }

        public void Update(TipoAlergeno tipoAlergeno)
        {
            _context.TiposAlergeno.Update(tipoAlergeno);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tipoAlergeno = GetById(id);
            if (tipoAlergeno != null)
            {
                _context.TiposAlergeno.Remove(tipoAlergeno);
                _context.SaveChanges();
            }
        }
    }
}
