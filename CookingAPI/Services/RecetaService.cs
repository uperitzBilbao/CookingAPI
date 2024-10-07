using CookingAPI.DataModel;
using CookingAPI.Interfaces;
using CookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingAPI.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly CookingModel _context;

        // Constructor para inyectar el contexto de la base de datos
        public RecetaService(CookingModel context)
        {
            _context = context;
        }

        // Obtener todas las recetas, incluyendo los ingredientes
        public List<Receta> GetAll()
        {
            return _context.Recetas.ToList();
        }

        // Obtener una receta por su ID
        public Receta? Get(int id)
        {
            return _context.Recetas.FirstOrDefault(r => r.IdReceta == id);
        }

        // Obtener todas las recetas, incluyendo los ingredientes
        public List<Receta> GetAllCompleto()
        {
            return _context.Recetas
            .Include(r => r.TipoDieta)
                .Include(r => r.TipoElaboracion)
                .Include(r => r.RecetaIngredientes)
                    .ThenInclude(ri => ri.Ingrediente).ThenInclude(ial => ial.IngredienteAlergenos).ThenInclude(aler => aler.TipoAlergeno)
                .ToList();
        }
        public Receta? GetCompleto(int id)
        {
            return _context.Recetas
                .Include(r => r.TipoDieta)
                .Include(r => r.TipoElaboracion)
                .Include(r => r.RecetaIngredientes)
                    .ThenInclude(ri => ri.Ingrediente).ThenInclude(ial => ial.IngredienteAlergenos).ThenInclude(aler => aler.TipoAlergeno)
                .FirstOrDefault(r => r.IdReceta == id);
        }


        // Agregar una nueva receta (incluyendo RecetaIngredientes)
        public void Add(Receta receta)
        {
            _context.Recetas.Add(receta);
            _context.SaveChanges();
        }

        // Actualizar una receta existente (incluyendo RecetaIngredientes)
        public void Update(int id, Receta updatedReceta)
        {
            var receta = Get(id);
            if (receta != null)
            {
                // Actualizar propiedades de la receta
                receta.Nombre = updatedReceta.Nombre;
                receta.IdTipoDieta = updatedReceta.IdTipoDieta;
                receta.Raciones = updatedReceta.Raciones;
                receta.Elaboracion = updatedReceta.Elaboracion;
                receta.Presentacion = updatedReceta.Presentacion;
                receta.IdTipoElaboracion = updatedReceta.IdTipoElaboracion;
                receta.TiempoMinutos = updatedReceta.TiempoMinutos;

                // Limpiar y volver a añadir los ingredientes
                receta.RecetaIngredientes.Clear();
                receta.RecetaIngredientes.AddRange(updatedReceta.RecetaIngredientes);

                _context.SaveChanges();
            }
        }

        // Eliminar una receta por su ID
        public void Delete(int id)
        {
            var receta = Get(id);
            if (receta != null)
            {
                _context.Recetas.Remove(receta);
                _context.SaveChanges();
            }
        }

        // Buscar recetas por nombre, tipo de dieta o alérgenos
        public List<Receta> Search(string? nombre, int? tipoDietaId, int? tipoAlergenoId)
        {
            var query = _context.Recetas.AsQueryable();

            // Incluir RecetaIngredientes e Ingredientes desde el principio
            query = query.Include(r => r.RecetaIngredientes)
                         .ThenInclude(ri => ri.Ingrediente); // Asegúrate de que la relación esté correctamente definida

            // Filtrar por nombre
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(r => r.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
            }

            // Filtrar por tipo de dieta
            if (tipoDietaId.HasValue)
            {
                query = query.Where(r => r.IdTipoDieta == tipoDietaId.Value);
            }

            // Filtrar por alérgenos
            if (tipoAlergenoId.HasValue)
            {
                query = query.Where(r => r.RecetaIngredientes
                    .Any(ri => ri.Ingrediente.IngredienteAlergenos
                        .Any(ia => ia.IdTipoAlergeno == tipoAlergenoId.Value)));
            }

            return query.ToList(); // Devolver la lista final de recetas
        }

    }
}
