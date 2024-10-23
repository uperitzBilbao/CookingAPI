using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;

namespace CookingAPI.Repositorio
{
    public class TipoDietaRepositorio : ITipoDietaRepositorio
    {
        private readonly CookingModel _context;

        public TipoDietaRepositorio(CookingModel context)
        {
            _context = context;
        }

        public void Add(TipoDieta tipoDieta)
        {
            _context.TiposDieta.Add(tipoDieta);
            _context.SaveChanges();
        }

        public TipoDieta GetById(int id)
        {
            return _context.TiposDieta.Find(id);
        }

        public TipoDieta GetByNombre(string nombre)
        {
            return _context.TiposDieta.FirstOrDefault(td => td.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<TipoDieta> GetAll()
        {
            return _context.TiposDieta.ToList();
        }

        public void Update(TipoDieta tipoDieta)
        {
            _context.TiposDieta.Update(tipoDieta);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tipoDieta = GetById(id);
            if (tipoDieta != null)
            {
                _context.TiposDieta.Remove(tipoDieta);
                _context.SaveChanges();
            }
        }
    }
}
