using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;

namespace CookingAPI.Repositorio
{
    public class TipoElaboracionRepositorio : ITipoElaboracionRepositorio
    {
        private readonly CookingModel _context;

        public TipoElaboracionRepositorio(CookingModel context)
        {
            _context = context;
        }

        public void Add(TipoElaboracion tipoElaboracion)
        {
            _context.TiposElaboracion.Add(tipoElaboracion);
            _context.SaveChanges();
        }

        public TipoElaboracion GetById(int id)
        {
            return _context.TiposElaboracion.Find(id);
        }

        public TipoElaboracion GetByNombre(string nombre)
        {
            return _context.TiposElaboracion.FirstOrDefault(te => te.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<TipoElaboracion> GetAll()
        {
            return _context.TiposElaboracion.ToList();
        }

        public void Update(TipoElaboracion tipoElaboracion)
        {
            _context.TiposElaboracion.Update(tipoElaboracion);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tipoElaboracion = GetById(id);
            if (tipoElaboracion != null)
            {
                _context.TiposElaboracion.Remove(tipoElaboracion);
                _context.SaveChanges();
            }
        }
    }
}
