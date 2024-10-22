using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingAPI.Models
{
    public class IngredienteAlergeno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Identificador único
        public int IdIngrediente { get; set; }
        public int IdTipoAlergeno { get; set; }


        public virtual Ingrediente? Ingrediente { get; set; } // Navegación hacia Ingrediente
        public virtual TipoAlergeno? TipoAlergeno { get; set; } // Navegación hacia TipoAlergeno
    }
}
