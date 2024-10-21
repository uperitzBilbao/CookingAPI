using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookingAPI.Models
{

    public class Receta
    {
        [Key]
        public int IdReceta { get; set; }

        [Required]
        [ForeignKey("TipoDieta")]
        public int IdTipoDieta { get; set; }
        public virtual TipoDieta TipoDieta { get; set; } // Propiedad de navegación

        public string Nombre { get; set; }
        public int Raciones { get; set; }
        public string Elaboracion { get; set; }
        public string Presentacion { get; set; }

        [Required]
        [ForeignKey("TipoElaboracion")]
        public int IdTipoElaboracion { get; set; }
        public virtual TipoElaboracion TipoElaboracion { get; set; } // Propiedad de navegación

        public int TiempoMinutos { get; set; }

        public List<RecetaIngrediente> RecetaIngredientes { get; set; } = new List<RecetaIngrediente>();
        public virtual ICollection<UsuarioReceta> UsuarioRecetas { get; set; } = new List<UsuarioReceta>();
    }


}
