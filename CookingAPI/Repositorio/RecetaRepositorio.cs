using CookingAPI.DataModel;
using CookingAPI.Interfaces;
using CookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Repositorio
{
    public class RecetaRepositorio : Repositorio<Receta>, IRecetaRepositorio
    {
        public RecetaRepositorio(CookingModel context) : base(context)
        {
        }

        public Receta GetRecetaCompleta(int id)
        {
            return _context.Recetas
                .Include(r => r.RecetaIngredientes)
                    .ThenInclude(ri => ri.Ingrediente)
                .Include(r => r.TipoDieta)
                .Include(r => r.TipoElaboracion)
                .FirstOrDefault(r => r.IdReceta == id);
        }

        public List<Receta> GetAllRecetasCompletas()
        {
            return _context.Recetas
                .Include(r => r.RecetaIngredientes)
                    .ThenInclude(ri => ri.Ingrediente)
                .Include(r => r.TipoDieta)
                .Include(r => r.TipoElaboracion)
                .ToList();
        }

        public List<Receta> SearchRecetas(string? nombre, int? tipoDietaId, int? tipoAlergenoId)
        {
            var query = _context.Recetas
                .Include(r => r.RecetaIngredientes)
                    .ThenInclude(ri => ri.Ingrediente)
                        .ThenInclude(i => i.IngredienteAlergenos)
                            .ThenInclude(ia => ia.TipoAlergeno)
                .AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(r => r.Nombre.Contains(nombre));
            }

            if (tipoDietaId.HasValue)
            {
                query = query.Where(r => r.IdTipoDieta == tipoDietaId.Value);
            }

            if (tipoAlergenoId.HasValue)
            {
                query = query.Where(r => r.RecetaIngredientes
                    .Any(ri => ri.Ingrediente.IngredienteAlergenos
                        .Any(ia => ia.IdTipoAlergeno == tipoAlergenoId.Value)));
            }

            return query.ToList();
        }
    }
}
