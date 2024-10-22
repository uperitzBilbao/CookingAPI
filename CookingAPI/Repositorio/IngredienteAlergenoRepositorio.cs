using CookingAPI.DataModel;
using CookingAPI.InterfacesRepo;
using CookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Repositorio
{
    public class IngredienteAlergenoRepositorio : IIngredienteAlergenoRepositorio
    {
        private readonly CookingModel _context;

        public IngredienteAlergenoRepositorio(CookingModel context)
        {
            _context = context;
        }

        public void Add(IngredienteAlergeno ingredienteAlergeno)
        {
            _context.IngredienteAlergenos.Add(ingredienteAlergeno);
            _context.SaveChanges();
        }

        public IngredienteAlergeno GetById(int id)
        {
            return _context.IngredienteAlergenos.Find(id);
        }

        public IEnumerable<IngredienteAlergeno> GetAll()
        {
            return _context.IngredienteAlergenos.Include(ia => ia.Ingrediente)
                                                  .Include(ia => ia.TipoAlergeno)
                                                  .ToList();
        }

        public void Update(IngredienteAlergeno ingredienteAlergeno)
        {
            _context.IngredienteAlergenos.Update(ingredienteAlergeno);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var ingredienteAlergeno = GetById(id);
            if (ingredienteAlergeno != null)
            {
                _context.IngredienteAlergenos.Remove(ingredienteAlergeno);
                _context.SaveChanges();
            }
        }
    }
}
