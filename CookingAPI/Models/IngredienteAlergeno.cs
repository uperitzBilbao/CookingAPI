using System.ComponentModel.DataAnnotations;

namespace CookingAPI.Models
{
    public class IngredienteAlergeno
    {
        [Key]
        public int Id { get; set; }  // Identificador único
        public int IdIngrediente { get; set; }
        public int IdTipoAlergeno { get; set; }


        public Ingrediente Ingrediente { get; set; } // Navegación hacia Ingrediente
        public TipoAlergeno TipoAlergeno { get; set; } // Navegación hacia TipoAlergeno
    }
}
