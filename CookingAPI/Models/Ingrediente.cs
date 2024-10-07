using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingAPI.Models
{

    public class Ingrediente
    {
        [Key]
        public int IdIngrediente { get; set; }
        public string Nombre { get; set; }
        [ForeignKey("TipoIngrediente")]
        public int IdTipoIngrediente { get; set; }
        public TipoIngrediente TipoIngrediente { get; set; } // Propiedad de navegación
        public List<IngredienteAlergeno> IngredienteAlergenos { get; set; } = new List<IngredienteAlergeno>();
    }

}
